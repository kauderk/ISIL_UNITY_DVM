using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game Data/WeaponSettings")]
    public class SO_WeaponSettings : ScriptableObject
    {
        public SO_WeaponShooter shooter;
        public SO_WeaponReloader reloader;
        public SO_AmmoSettings bulletSettings;
        public SO_WeaponMagazine Magazine;
        public SO_WeaponSFX SFX;
    }
}