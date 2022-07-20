public enum Players
{
    Player1,
    Player2,
    Player3,
    Player4
}

[System.Serializable]
public class PlayerStats
{
    public int KillCount;
    public int DeathCount;
    public int Score;
    public int RespawnCount;
}
// TODO: add the PlayerStats class