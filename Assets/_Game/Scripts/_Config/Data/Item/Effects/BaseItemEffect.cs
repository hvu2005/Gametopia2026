using UnityEngine;

/// <summary>
/// Lớp cơ sở trừu tượng cho tất cả hiệu ứng của Item.
/// Kế thừa class này để tạo các loại Effect mới mà KHÔNG cần sửa code nào khác.
///
/// Quy ước:
/// - Tạo file SO độc lập với [CreateAssetMenu] cho mỗi loại Effect
/// - Các Effect phổ thông nên đặt tại Data/Effects/Common để share giữa nhiều Item
/// - Mỗi class con chỉ override hook nào nó cần — các hook khác mặc định là no-op
/// </summary>
public abstract class BaseItemEffect : ScriptableObject
{
    [Header("Effect Info")]
    [Tooltip("Tên hiển thị của effect (dùng trong Tooltip UI)")]
    public string EffectName;

    [TextArea(1, 3)]
    [Tooltip("Mô tả hiệu ứng — hiện trong Item Tooltip")]
    public string EffectDescription;

    // ─── Lifecycle Hooks ─────────────────────────────────────────────────────

    /// <summary>
    /// Gọi khi item được equip vào Player.
    /// Đây là nơi đăng ký Modifier, bật buff, đăng ký EventBus listener.
    /// </summary>
    public abstract void OnEquip(ItemInstance item, Player owner);

    /// <summary>
    /// Gọi khi item bị unequip khỏi Player.
    /// Đây là nơi xóa Modifier, tắt buff, hủy đăng ký EventBus listener.
    /// </summary>
    public abstract void OnUnequip(ItemInstance item, Player owner);

    // ─── Battle Hooks (optional override) ────────────────────────────────────

    /// <summary>Gọi khi trận đấu bắt đầu. Override nếu effect cần khởi tạo trạng thái battle.</summary>
    public virtual void OnBattleStart(ItemInstance item, Player owner) { }

    /// <summary>Gọi sau mỗi lần Player thực hiện tấn công.</summary>
    public virtual void OnOwnerAttack(ItemInstance item, Player owner, BaseEntity target) { }
}
