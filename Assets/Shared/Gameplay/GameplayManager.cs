using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using System;

[RequireComponent(typeof(PhotonView))]
public class GameplayManager : MonoBehaviourPun
{
    public static GameplayManager Instance { get; private set; }
    public UDictionary<Players, PlayerStats> PlayerDataDict = new UDictionary<Players, PlayerStats>();

    int creationCount = -1;
    PhotonView photonViewComp;

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        photonViewComp = GetComponent<PhotonView>();
    }

    // get next empty key
    public PlayerStats RequestNextPlayer()
    {
        var nextKey = (Players)PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log($"Player {nextKey} is created");
        return new PlayerStats(nextKey, PhotonNetwork.NickName);
    }
}
