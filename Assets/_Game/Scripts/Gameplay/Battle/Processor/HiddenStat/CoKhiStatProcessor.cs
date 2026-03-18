




using System.Collections.Generic;
using UnityEngine;

public class CoKhiStatProcessor : BaseStatProcessor, IPostAttack
{

    // Đếm số lần source đã thực hiện đòn đánh (theo vòng đời processor).
    private static readonly Dictionary<BaseEntity, int> _attackCountByEntity = new();

    public void ProcessPostAttack(BaseEntity source, BaseEntity target, List<BaseEntity> allAliveEnemies = null)
    {
        if (source == null || target == null) return;
        if (source.IsDead || target.IsDead) return;

        if (!_attackCountByEntity.TryGetValue(source, out int count))
            count = 0;
        count++;
        _attackCountByEntity[source] = count;

        // Cơ khí: ở lượt đánh thứ ba áp dụng hiệu ứng Làm Choáng lên kẻ địch chịu đòn.
        if (count % 3 != 0) return;

        new CoKhiStunEffect().ApplyEffect(target);
        Debug.Log($"[HiddenStat:CoKhi] {source.name} kích hoạt đòn {count} → gọi CoKhiStunEffect lên {target.name}");
    }
}