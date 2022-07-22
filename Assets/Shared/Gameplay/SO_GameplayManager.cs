using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;

[CreateAssetMenu(fileName = "GameplayManager", menuName = "Managers/GameplayManager")]
public class GameplayManager : SingletonScriptableObject<GameplayManager>
{
    public UDictionary<Players, PlayerStats> PlayerDataDict = new UDictionary<Players, PlayerStats>();

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
