

using System.Collections.Generic;
using UnityEngine;


public class PhysicalDamageProcessor : BaseStatProcessor, IOnAttack
{
    public void ProcessOnAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        Debug.Log("Processing Physical Damage" + source.Stats.physicalDamage);
        var damage = source.Stats.physicalDamage;
        target.currentHp -= damage;
        source.lastDamageDealt = damage; // ghi lại để SuckBlood, CounterAttack dùng

        FloatingTextType damageType = FloatingTextType.PhysicalDamage;
        
        // Kiểm tra xem cú đánh này có phải là chí mạng không dựa vào StatProcessSystem
        if (StatProcessSystem.currentCritEntities.Contains(source))
        {
            damageType = FloatingTextType.CriticalDamage;
            StatProcessSystem.currentCritEntities.Remove(source); // Dọn dẹp trạng thái sau khi sử dụng
        }

        if (damage > 0)
        {
            EventBus.Emit(BattleEventType.SpawnFloatingText, new FloatingTextEventData
            {
                Target = target,
                Value = damage,
                Type = damageType,
                OffsetBuffer = Vector2.zero
            });
        }
    }
}