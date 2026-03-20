using UnityEngine;

public class TrashSlot : Slot
{
    public override bool IsEmpty()
    {
        // Luôn trả về true để cho phép kéo thả item vào ô này từ mọi nơi.
        return true;
    }

    public override void PlaceItem(Item item)
    {
        // Khi người chơi thả item vào TrashSlot, ta không lưu trữ item đó.
        // Thay vào đó, ta destroy item để despawn.
        Destroy(item.gameObject);

        // Phát âm thanh khi vứt item (có thể tùy chỉnh lại audio key nếu muốn)
        if (AudioController.Instance != null)
        {
            AudioController.Instance.PlaySfx(AudioController.AudioKeys.UiClick);
        }
    }
}
