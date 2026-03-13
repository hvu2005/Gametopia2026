using UnityEngine;

/// <summary>
/// Thủ (Giáp): Sát thương hấp thụ vào currentDef trước.
/// Chỉ khi giáp = 0 thì phần sát thương dư mới trừ vào HP.
/// Giáp hồi về đầy (= Stats.def) sau khi kết thúc mỗi combat (BattleManager gọi RestoreDefense).
/// 
/// Hoạt động bằng cách chỉnh physicalDamage của attacker thành phần dư (overflow) ở PreAttack,
/// để PhysicalDamageProcessor áp dụng đúng lượng damage lên HP. PostAttack khôi phục lại.
/// </summary>
public class DefenseProcessor : BaseStatProcessor, IPreAttack, IPostAttack
{
    private float originalPhysicalDamage;

    public void ProcessPreAttack(BaseEntity source, BaseEntity target)
    {
        // Không có giáp → bỏ qua
        if (target.currentDef <= 0) return;

        originalPhysicalDamage = source.Stats.physicalDamage;
        float damage = source.Stats.physicalDamage;

        if (damage <= target.currentDef)
        {
            // Giáp hấp thụ toàn bộ, HP không bị trừ
            target.currentDef -= damage;
            source.Stats.physicalDamage = 0;
            Debug.Log($"[Defense] {target.name} giáp hấp thụ {damage} sát thương → còn {target.currentDef} giáp");
        }
        else
        {
            // Giáp bị bào mòn hết, phần dư vào HP
            float overflow = damage - target.currentDef;
            Debug.Log($"[Defense] {target.name} giáp bị phá! Còn lại {overflow} sát thương xuyên vào HP");
            target.currentDef = 0;
            source.Stats.physicalDamage = overflow;
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        if (originalPhysicalDamage != 0)
        {
            source.Stats.physicalDamage = originalPhysicalDamage;
            originalPhysicalDamage = 0;
        }
    }
}
