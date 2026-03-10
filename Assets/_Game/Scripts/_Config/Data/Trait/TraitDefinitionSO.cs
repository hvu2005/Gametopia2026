using UnityEngine;

/// <summary>
/// Định nghĩa một Tộc/Hệ (Synergy Trait) bao gồm các mốc kích hoạt.
/// Tạo 1 file riêng cho mỗi Trait (Machine, Warrior...) trong thư mục Data/Traits.
/// </summary>
[CreateAssetMenu(fileName = "Trait_", menuName = "Game/Trait Definition")]
public class TraitDefinitionSO : ScriptableObject
{
    [Header("Identity")]
    public SynergyType TraitType;
    public string DisplayName;
    [TextArea(1, 2)] public string Description;
    public Sprite Icon;

    [Header("Milestones")]
    [Tooltip("Danh sách mốc kích hoạt, sắp xếp tăng dần theo RequiredCount.")]
    public TraitMilestone[] Milestones;
}

/// <summary>Mốc kích hoạt khi đủ số lượng Trait yêu cầu.</summary>
[System.Serializable]
public class TraitMilestone
{
    [Tooltip("Cần bao nhiêu item cùng Trait để kích hoạt mốc này.")]
    public int RequiredCount;

    [Tooltip("Mô tả bonus — hiện trong Synergy UI.")]
    [TextArea(1, 2)]
    public string Description;

    [Tooltip("Effect kích hoạt khi đạt mốc. Thường là StatEffect hoặc TriggerEffect.")]
    public BaseItemEffect MilestoneEffect;
}
