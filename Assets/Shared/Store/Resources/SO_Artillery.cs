using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Weapon;

namespace Store
{
    [CreateAssetMenu(fileName = "Artillery", menuName = "Store/Artillery")]
    public class SO_Artillery : SingletonScriptableObject<SO_Artillery>
    {
        [Tooltip("SO_SceneData are ScriptableObjects")]
        public UDictionary<TYPEWEAPON, SO_AmmoSettings> Ammo;
    }
}
