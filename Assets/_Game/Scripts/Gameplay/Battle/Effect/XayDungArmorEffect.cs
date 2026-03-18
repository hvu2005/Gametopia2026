using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// HiddenStat - Xây dựng: +5 giáp mỗi lần bị đánh trúng, tối đa 5 stack.
/// Effect tự quản lý stack theo từng BaseEntity.
/// </summary>
public class XayDungArmorEffect : BaseEffect
{
    private const int MaxStacks = 5;
    private const float ArmorPerStack = 5f;

    private static readonly Dictionary<BaseEntity, int> _stacksByDefender = new();

    public override void ApplyEffect(BaseEntity target)
    {
        if (target == null || target.IsDead) return;

        if (!_stacksByDefender.TryGetValue(target, out int stacks))
            stacks = 0;

        if (stacks >= MaxStacks) return;

        stacks++;
        _stacksByDefender[target] = stacks;

        float maxArmorWithStacks = target.Stats.armor + stacks * ArmorPerStack;
        float before = target.armor;
        target.armor = Mathf.Min(target.armor + ArmorPerStack, maxArmorWithStacks);

        Debug.Log($"[Effect:XayDung] {target.name} stack {stacks}/{MaxStacks} → +{target.armor - before:F1} giáp (giáp {target.armor:F1}, trần {maxArmorWithStacks:F1})");
    }

    /// <summary>
    /// Cho phép reset stack khi cần (ví dụ kết thúc combat).
    /// </summary>
    public static void ResetStacks(BaseEntity target)
    {
        if (target == null) return;
        _stacksByDefender.Remove(target);
    }

    public static void ResetAllStacks()
    {
        _stacksByDefender.Clear();
    }
}
