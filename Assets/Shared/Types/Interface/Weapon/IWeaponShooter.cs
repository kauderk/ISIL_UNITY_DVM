using UnityEngine;

namespace Weapon
{
    public interface IWeaponShooter
    {
        public WeaponType Type { get; } // The actual Weapon should have this property.
        public int FireRate { get; }
        public float Cadence { get; }
        public GameObject Bullet { get; }
        public void Fire();
        public void Init(SOC_WeaponShooter EditorSettings);
        public bool CanFire(float deltaFireRate);
    }
    [System.Serializable]
    public class SOC_WeaponShooter
    {
        // FIXME:
        [field: SerializeField]
        public Transform Scope { get; private set; }

        [field: SerializeField]
        public GameObject Caster { get; private set; }
    }
}
