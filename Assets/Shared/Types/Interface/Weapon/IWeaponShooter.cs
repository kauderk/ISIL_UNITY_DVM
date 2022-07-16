using System;
using UnityEngine;

namespace Weapon
{
    public interface IWeaponShooter
    {
        WeaponType Type { get; } // The actual Weapon should have this property.
        int Burst { get; }
        float Cadence { get; }
        float Delay { get; }
        void Fire(Action OnBurst = null, float delayInSeconds = 0);
        void Init(SO_WeaponMagazine Magazine, SO_AmmoSettings BulletSettings, SOC_WeaponShooter EditorSettings);
        bool Elapsed(float deltaFireRate);
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
