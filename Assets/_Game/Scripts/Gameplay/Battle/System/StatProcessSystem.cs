
using System.Collections.Generic;
using UnityEngine;

public class StatProcessSystem
{
    public List<BaseStatProcessor> statProcessors = new();

    public StatProcessSystem()
    {
        // --- PreAttack phase (thứ tự quan trọng) ---
        // 1. Dodge trước: nếu né thành công → physicalDamage = 0, các bước sau không có tác dụng
        statProcessors.Add(new DodgeProcessor());
        // 2. Crit: nhân physicalDamage nếu chí mạng
        statProcessors.Add(new CritProcessor());
        // 3. Khuếch đại sát thương: nhân thêm % sau khi crit đã tính
        statProcessors.Add(new DamageAmpProcessor());
        // 4. Giáp: trừ def của mục tiêu ra khỏi physicalDamage
        statProcessors.Add(new DefenseProcessor());

        // --- OnAttack phase ---
        // 5. Gây sát thương vật lý thực sự
        statProcessors.Add(new PhysicalDamageProcessor());

        // --- PostAttack phase ---
        // 6. Choáng
        statProcessors.Add(new StunProcessor());
        // 7. Hút máu (flat, dùng Stats.lifeSteal)
        statProcessors.Add(new LifeStealEffect());
        // 8. Hút máu % sát thương (dùng Stats.suckBlood)
        // statProcessors.Add(new SuckBloodProcessor());
        // 9. Độc
        statProcessors.Add(new PoisonProcessor());
        // 10. Phản đòn: target phản % sát thương nhận vào
        statProcessors.Add(new CounterAttackProcessor());
        // 11. Tốc độ đánh: cơ hội đánh thêm 1 đòn
        statProcessors.Add(new AttackSpeedProcessor());
        // 12. May mắn: tăng bậc rarity loot khi kẻ địch chết
        statProcessors.Add(new LuckyProcessor());
    }

    public void ProcessPreAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IPreAttack stage)
                stage.ProcessPreAttack(source, target);
        }
    }

    public void ProcessOnAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IOnAttack stage)
                stage.ProcessOnAttack(source, target);
        }
    }

    public void ProcessPostAttack(BaseEntity source, BaseEntity target)
    {
        foreach (var p in statProcessors)
        {
            if (p is IPostAttack stage)
                stage.ProcessPostAttack(source, target);
        }
    }
}