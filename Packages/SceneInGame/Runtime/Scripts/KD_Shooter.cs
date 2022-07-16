using System;
using UnityEngine;

namespace Weapon
{
    public class KD_Shooter : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;

        IWeaponShooter IShooter;

        protected override void MyAwake()
        {
            base.MyAwake();
            delta = Settings.FireRate + 1; // able to shoot on start up
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        protected override void MyUpdate()
        {
            if (Magazine.Busy)
                return;

            var input = this.InputShoot();

            if (pending)
                delta += Time.deltaTime;

            if (input.down)
            {
                if (!Magazine.HasAmmo())
                    Play(S => S.Empty);
                else if (pending)
                    Play(S => S.Dry);
            }
            else if (input.up)
            {
                Play(S => S.Release);
                transform.NotifySiblings<IFireEvent>(I => I.OnStopFire());
            }
            if (IShooter.Elapsed(delta))
            {
                Resolve();
            }

            var cycle = IShooter.CanShoot(input.down, input.hold, Magazine);

            if (!pending && cycle.canShoot && Magazine.HasAmmo())
            {
                Awaiting();

                cycle.TakeFromMagazine();
                IShooter.Fire(OnBurst: () => Play(S => S.Burst));
                Play(S => S.Fire);

                transform.NotifySiblings<IFireEvent>(I => I.OnFire());
            }
        }
    }
}
