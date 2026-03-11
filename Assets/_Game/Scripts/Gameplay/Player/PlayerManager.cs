


[System.Serializable]
public class PlayerManager
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
}