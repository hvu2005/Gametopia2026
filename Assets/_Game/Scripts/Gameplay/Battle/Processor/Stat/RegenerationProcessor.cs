using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hồi phục (regeneration): hồi HP cho người tấn công ở PreAttack (trước khi ra đòn).
/// Implement giống LifeStealProcessor nhưng chạy ở IPreAttack.
/// Giá trị dùng dạng flat (ví dụ regeneration = 5 => hồi 5 HP trước mỗi lượt đánh).
/// </summary>
public class RegenerationProcessor : BaseStatProcessor, IPreAttack
{
    public void ProcessPreAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source.Stats.regeneration > 0)
        {
            source.Heal(source.Stats.regeneration);
            Debug.Log($"[Regeneration] {source.name} hồi phục {source.Stats.regeneration} HP trước khi tấn công");
        }
    }
}
