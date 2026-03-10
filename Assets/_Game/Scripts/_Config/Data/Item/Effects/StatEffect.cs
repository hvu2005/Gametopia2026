using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Effect chuyên dụng để cộng Stat Modifier vào Player.
/// Đây là Effect phổ biến nhất — dùng cho mọi item có buff chỉ số đơn giản.
///
/// Tip: Tạo một file StatEffect SO ở Common/ và dùng chung cho nhiều item.
/// Thay đổi Value ở 1 nơi → tất cả item dùng effect đó được cập nhật.
/// </summary>
[CreateAssetMenu(fileName = "StatEffect_", menuName = "Game/Effects/Stat Effect")]
public class StatEffect : BaseItemEffect
{
    [Header("Modifiers")]
    [Tooltip("Danh sách modifier để cộng vào stat. Có thể nhiều modifier khác loại.")]
    public List<StatModifier> Modifiers = new();

    public override void OnEquip(ItemInstance item, Player owner)
    {
        if (owner == null || owner.StatManager == null) return;

        string sourceID = item != null ? item.Guid : $"effect_{GetInstanceID()}";

        foreach (var mod in Modifiers)
        {
            var boundMod = new StatModifier(mod.StatType, mod.Value, mod.ModifierType, sourceID);
            owner.StatManager.AddModifier(boundMod);
        }
    }

    public override void OnUnequip(ItemInstance item, Player owner)
    {
        if (owner == null || owner.StatManager == null) return;

        string sourceID = item != null ? item.Guid : $"effect_{GetInstanceID()}";
        owner.StatManager.RemoveModifiers(sourceID);
    }
}
