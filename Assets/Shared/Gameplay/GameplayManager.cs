using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;


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
        //RPCSetCreationCount();
    }

    private void RPCSetCreationCount()
    {
        photonViewComp.RPC("SetCreationCount", RpcTarget.All, creationCount);
    }

    [PunRPC]
    void SetCreationCount(int count)
    {
        creationCount = count;
    }

    // get next empty key
    public PlayerStats RequestNextPlayer()
    {
        // get next empty position
        // var keys = PlayerDataDict.Keys;
        // var nextKey = keys.First(K => !PlayerDataDict.ContainsKey(K));
        Players lastKey = PlayerDataDict.Keys.ToList().Count != 0 ? PlayerDataDict.Keys.Last() : Players.Player1;
        if (PhotonNetwork.IsMasterClient)
        {
            // get last PlayerDataDict key
            if (PlayerDataDict.ContainsKey(lastKey) && PlayerDataDict[lastKey] != null)
            {
                Debug.Log("Server: Returning last player");
                return PlayerDataDict[lastKey]; // I'm the server
            }
            else
            {
                creationCount++;
                RPCSetCreationCount();
                Debug.Log("Server: Creating new player");
                return CreateNewStats(creationCount); // I'm the server but I don't have a player yet
            }
        }
        if (PlayerDataDict.ContainsKey(lastKey) && PlayerDataDict[lastKey] != null)
        {
            Debug.Log("Client: Returning last player");
            return PlayerDataDict[lastKey]; // I'm the server
        }
        else
        {
            Debug.Log("Client: Creating player");
            return CreateNewStats(creationCount); // I'm the server but I don't have a player yet
        }
    }

    private PlayerStats CreateNewStats(int count)
    {
        var nextKey = (Players)count;
        PlayerDataDict[nextKey] = new PlayerStats(nextKey, PhotonNetwork.NickName);
        Debug.Log($"Player {nextKey} is created");
        return PlayerDataDict[nextKey];
    }
}
