using System.Collections.Generic;
using UnityEngine;

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
    public Players ID { get; }
    public int KillCount { get; }
    public int DeathCount { get; }
    public int Score { get; }
    public int RespawnCount { get; }

    [field: SerializeField]
    public float Health { get; set; } = 100f; // should be private...
    public PlayerInstance Instance { get; } = new PlayerInstance();
}
public class PlayerInstance
{
    public GameObject Player;
    public GameObject Camera;
}
// TODO: add the PlayerStats class