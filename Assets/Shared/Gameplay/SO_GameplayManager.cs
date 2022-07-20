using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "GameplayManager", menuName = "Managers/GameplayManager")]
public class GameplayManager : SingletonScriptableObject<GameplayManager>
{
    public UDictionary<Players, PlayerStats> PlayerDataDict = new UDictionary<Players, PlayerStats>();
}
