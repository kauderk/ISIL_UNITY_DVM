using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponMagazine", menuName = "Game Data/WeaponMagazine")]
    public class SO_WeaponMagazine : ScriptableObject, IWeaponMagazine
    {
        [field: SerializeField]
        public int MagazineSize { get; private set; } = 10;

        [field: SerializeField]
        public int Amoution { get; private set; } = 10;

        [field: SerializeField]
        public TYPEWEAPON Type { get; private set; }

        public bool Busy { get; set; }

        public void Consume(int amount = 1) => Amoution -= amount;
        public void Load(int amount = 1) => Amoution += amount;
        public void Fill() => Amoution = MagazineSize;
        public bool HasAmmo() => Amoution > 0;
        public void Clear() => Amoution = 0;
    }
}
