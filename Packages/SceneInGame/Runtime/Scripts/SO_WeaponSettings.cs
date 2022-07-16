using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game Data/WeaponSettings")]
    public class SO_WeaponSettings : ScriptableObject
    {
        public SO_WeaponShooter Shooter;
        public SO_WeaponReloader Reloader;
        public SO_AmmoSettings Ammo;
        public SO_WeaponMagazine Magazine;
        public SO_WeaponSkin Skin;
        public SO_WeaponSFX SFX;
    }
}
