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
public class PlayerStats // should be private...
{
    public static readonly int MaxLives = 5;
    public static readonly int MaxHealth = 100;
    public Players ID { get; private set; }
    public string NickName { get; private set; }
    public int KillCount { get; private set; }
    public int DeathCount { get; private set; }
    public int Score { get; private set; }
    public int RespawnCount { get; private set; }

    public PlayerStats(string nickName = "Player")
    {
        NickName = nickName;
    }

    [field: SerializeField]
    public float Health { get; set; } = 100f;
    public PlayerInstance Instance { get; } = new PlayerInstance();

    public void OnDead()
    {
        DeathCount++;
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
        if (RespawnCount > MaxLives)
        {
            Debug.LogWarning("Player " + ID + " has no more respawns");
            return false;
        }
        var t = DeathCount / MaxLives;
        Health = MaxHealth - (MaxHealth * t);
        return true;
    }
}
public class PlayerInstance
{
    public GameObject Player;
    public GameObject Camera;
}
// TODO: add the PlayerStats class