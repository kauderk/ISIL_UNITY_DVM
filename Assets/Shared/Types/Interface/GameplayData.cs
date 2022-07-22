using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public interface IMultiplayerSubscriber : IGlobalSubscriber
{
    void OnPlayerInstaceCreated(PlayerStats Player);
}

public enum Players
{
    Player1,
    Player2,
    Player3,
    Player4
}

[System.Serializable]
public class PlayerStats // should be private...
{
    public const int MaxLives = 5;
    public static readonly int MaxHealth = 100;
    public static float[] SpawnTimes = new float[MaxLives] { 1f, 1.5f, 2f, 2.5f, 3f };
    public float GetSpawnTime() => SpawnTimes[DeathCount];

    public Players ID { get; }
    public int Order { get; }
    public string NickName { get; }
    public int KillCount { get; private set; }
    public int DeathCount { get; private set; }
    public int Score { get; private set; }
    public int RespawnCount { get; private set; }

    public PlayerStats(Players id, string nickName = "Player")
    {
        NickName = nickName;
        ID = id;
        Order = (int)id + 1;
    }

    [field: SerializeField]
    public float Health { get; set; } = 100f;
    public PlayerInstance Instance { get; } = new PlayerInstance();

    public void OnDead()
    {
        DeathCount += 1 % MaxLives; // alright...
    }
    public void OnConfirmedKill()
    {
        KillCount++;
    }
    public void OnScore()
    {
        Score++;
    }
    public bool OnTryingToRespawn()
    {
        DeathCount++;
        if (RespawnCount > MaxLives) // try
        {
            Debug.LogWarning("Player " + ID + " has no more respawns");
            return false;
        }
        var t = DeathCount / MaxLives;
        Health = MaxHealth - (MaxHealth * t);
        RespawnCount++; // success
        return true;
    }
}
public class PlayerInstance
{
    public GameObject Player;
    public GameObject Camera;
}
// TODO: add the PlayerStats class