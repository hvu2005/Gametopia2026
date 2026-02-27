using System.Collections.Generic;

public abstract class BaseRunManager {
    public int CurrentFloor { get; protected set; }
    public int CorruptionLevel { get; protected set; } // Độ khó
    
    public abstract void GenerateNextEncounter(); // Tạo quái vật/Sự kiện
    public abstract List<BaseItem> GenerateLootDrop(); // Rớt đồ ngẫu nhiên sau combat
    public abstract void GenerateSkillTreeOptions(); // Roll 3 kỹ năng ngẫu nhiên khi lên cấp
}