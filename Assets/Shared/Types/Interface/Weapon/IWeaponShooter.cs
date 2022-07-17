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
        bool Elapsed(float deltaFireRate);
        void Init(SO_AmmoSettings Ammo = null, SOC_WeaponShooter Editor = null, SO_WeaponSkin skin = null);
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
