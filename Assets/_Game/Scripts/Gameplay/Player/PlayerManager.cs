


[System.Serializable]
public class PlayerManager : EventEmitter
{
    public Player player;

    public void IncreasePlayerStats(Stats stats)
    {
        player.IncreaseStats(stats);
    }

    public void DecreasePlayerStats(Stats stats)
    {
        player.DecreaseStats(stats);
    }

    public void EquipItem(Item item)
    {
        IncreasePlayerStats(item.stats);
    }

    public void UnequipItem(Item item)
    {
        DecreasePlayerStats(item.stats);
    }

    public void ResetPlayer()
    {
        player.ActiveEffects.Clear();
        player.currentArmor = player.Stats.armor;
        player.currentHp = player.Stats.hp;
    }
}