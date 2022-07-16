using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponReloader", menuName = "Game Data/WeaponReloader")]
    public class SO_WeaponReloader : ScriptableObject, KD_IWeaponReloader, IBaseWeaponCmp
    {
        [field: SerializeField]
        public int reloadAmount { get; private set; }

        [field: SerializeField]
        public float reloadTime { get; private set; }

        public SO_WeaponMagazine Magazine { get; private set; }
        public SO_WeaponSFX SFX { get; private set; }

        public void Init(SO_WeaponMagazine Magazine, SO_WeaponSFX SFX)
        {
            this.Magazine = Magazine;
            this.SFX = SFX;
        }

        public void FillMagazine() => Magazine.fill();

        public void Reload() // reloaing one by one could be a mechanic in it's own right
        {
            FillMagazine();
        }
    }
}
