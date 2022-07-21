using UnityEngine;
using Photon.Pun;
using System;
using Weapon;

public abstract class WeaponMonoBehaviourPunBase : MonoBehaviourPunBase // think of this as an annoying "div" wrapper
{
    [field: SerializeField, DimmerAssign, Tooltip("Will render Settings, if this isn't null.")]
    public SO_WeaponSettings WeaponSettings { get; set; }
}

namespace Weapon
{
    public abstract class KD_MonoWeapon : WeaponMonoBehaviourPunBase, IPlayerStatsSubscriber, ICollisionSubscriber
    {
        //public SO_WeaponSettings WeaponSettings { get; }
        protected SO_WeaponMagazine Magazine;
        protected float delta;
        protected bool pending;
        protected PlayerStats Stats;

        protected override void MyAwake() => UpdateMagazine();

        private void UpdateMagazine() => Magazine = WeaponSettings.Magazine;

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

        public void OnStatsChanged(PlayerStats stats) => Stats = stats;

        public void OnCollisionWithMagazine(SO_WeaponSettings weaponSettings, Collision collision)
        {
            WeaponSettings = weaponSettings;
            UpdateMagazine();
            OnNewWeaponSettings();
        }
        protected virtual void OnNewWeaponSettings() { }
    }
}