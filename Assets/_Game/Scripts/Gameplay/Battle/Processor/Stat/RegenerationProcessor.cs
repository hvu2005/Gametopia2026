using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Hồi phục (regeneration): hồi HP cho người tấn công ở PreAttack (trước khi ra đòn).
/// Implement giống LifeStealProcessor nhưng chạy ở IPreAttack.
/// Giá trị dùng dạng flat (ví dụ regeneration = 5 => hồi 5 HP trước mỗi lượt đánh).
/// </summary>
public class RegenerationProcessor : BaseStatProcessor, IPostAttack
{
    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (target.Stats.regeneration > 0)
        {
            target.Heal(target.Stats.regeneration);
            Debug.Log($"[Regeneration] {target.name} hồi phục {target.Stats.regeneration} HP trước khi tấn công");
        }
    }
}
