using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;
using System.Collections.Generic;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance { get; private set; }
    public UDictionary<Players, PlayerStats> PlayerDataDict = new UDictionary<Players, PlayerStats>();

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

    // get next empty key
    public Players GetNextEmptyKey()
    {
        var keys = PlayerDataDict.Keys;
        var nextKey = keys.FirstOrDefault(K => !PlayerDataDict.ContainsKey(K));
        if (nextKey == default)
        {
            Debug.LogError("No empty key found");
            return default;
        }
        return nextKey;
    }
}
