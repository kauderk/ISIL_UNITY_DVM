using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using Weapon;

namespace Store
{
    [System.Serializable]
    public class UDisctionary<Tkey, Tval> : SerializableDictionaryBase<Tkey, Tval> { }

    [CreateAssetMenu(fileName = "Artillery", menuName = "Store/Artillery")]
    public class SO_Artillery : SingletonScriptableObject<SO_Artillery>
    {
        [Tooltip("SO_SceneData are ScriptableObjects")]
        public UDisctionary<TYPEWEAPON, SO_AmmoSettings> Ammo;
    }
}
