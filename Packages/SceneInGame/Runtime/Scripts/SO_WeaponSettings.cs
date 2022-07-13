using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game Data/WeaponSettings")]
    public class SO_WeaponSettings : ScriptableObject
    {
        public SO_WeaponShooter shooter;
        public SO_WeaponReloader reloader;
        public SO_BulletSettings bulletSettings;
    }
}