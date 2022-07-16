using UnityEngine;
using Photon.Pun;
using System;

namespace Weapon
{
    public abstract class KD_MonoWeapon : MonoBehaviourPunBase
    {
        [DimmerAssign, Tooltip("Will render Settings, if this isn't null.")]
        public SO_WeaponSettings WeaponSettings;
        protected SO_WeaponMagazine Magazine;
        protected float delta;
        protected bool pending;

        protected override void MyAwake()
        {
            Magazine = WeaponSettings.Magazine;
        }

        public void Play(Func<SO_WeaponSFX, AudioClip> cb) => WeaponSettings.SFX.Play(cb);
        protected void Awaiting(bool? BusyMagazine = null)
        {
            WeaponSettings.Magazine.Busy = BusyMagazine ?? WeaponSettings.Magazine.Busy;
            pending = true;
        }
        protected void Resolve(bool? BusyMagazine = null)
        {
            WeaponSettings.Magazine.Busy = BusyMagazine ?? WeaponSettings.Magazine.Busy;
            delta = 0;
            pending = false;
        }
    }
}