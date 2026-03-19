using UnityEngine;

public struct FloatingTextEventData
{
    public BaseEntity Target;
    public float Value;
    public FloatingTextType Type;
    public Vector2 OffsetBuffer; // Buffer X Y để có thể điều chỉnh vị trí tùy biến từ nơi spawn nếu muốn, hoặc mặc định 0 0
}
