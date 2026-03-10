using UnityEngine;

/// <summary>
/// Lớp cơ sở cho các Effect có logic đặc biệt dựa trên sự kiện (Trigger-based).
/// KHÔNG dùng class này trực tiếp — tạo subclass cho từng loại trigger cụ thể.
///
/// Ví dụ tạo subclass:
///   [CreateAssetMenu(menuName = "Game/Effects/Stun On Third Hit")]
///   public class StunOnThirdHitEffect : TriggerEffect { ... }
///
/// Thiết kế: EventBus được đăng ký trong OnEquip và hủy trong OnUnequip,
/// đảm bảo không leak listener khi item bị bỏ ra.
/// </summary>
public abstract class TriggerEffect : BaseItemEffect
{
    // ─── State (không serialize — reset mỗi lần equip) ───────────────────────

    protected ItemInstance _boundItem;
    protected Player _owner;

    // ─── Lifecycle ───────────────────────────────────────────────────────────

    public override void OnEquip(ItemInstance item, Player owner)
    {
        _boundItem = item;
        _owner = owner;
        RegisterListeners();
    }

    public override void OnUnequip(ItemInstance item, Player owner)
    {
        UnregisterListeners();
        _boundItem = null;
        _owner = null;
    }

    // ─── Abstract Hooks ──────────────────────────────────────────────────────

    /// <summary>Đăng ký EventBus listener tại đây. Gọi từ OnEquip.</summary>
    protected abstract void RegisterListeners();

    /// <summary>Hủy đăng ký EventBus listener tại đây. Gọi từ OnUnequip.</summary>
    protected abstract void UnregisterListeners();
}
