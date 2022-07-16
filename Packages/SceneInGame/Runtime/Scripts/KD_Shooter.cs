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
        float delta;
        bool pending;
        SO_WeaponMagazine mag;

        protected override void MyAwake()
        {
            delta = Settings.FireRate + 1; // able to shoot on start up
            mag = Settings.Magazine;
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        protected override void MyUpdate()
        {
            if (mag.Busy)
                return;

            var input = InputShoot();

            if (pending)
                delta += Time.deltaTime;

            if (input.down)
            {
                if (!mag.HasAmmo())
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

            var cycle = CanShoot(input.down, input.hold);

            if (!pending && cycle.canShoot && mag.HasAmmo())
            {
                Awaiting();

                cycle.TakeFromMag();
                IShooter.Fire(OnBurst: () => Play(S => S.Burst));
                Play(S => S.Fire);

                transform.NotifySiblings<IFireEvent>(I => I.OnFire());
            }
        }

        private (bool canShoot, Action TakeFromMag) CanShoot(bool down, bool hold)
        {
            var automatic = hold && (IShooter.Type == WeaponType.automatic);
            var semiAutoOrSingle = down && (IShooter.Type != WeaponType.automatic); // so far there are only three
            var canShoot = automatic || semiAutoOrSingle;
            void TakeFromMag()
            {
                if (IShooter.Type == WeaponType.singleShot)
                    mag.Clear();
                else
                    mag.Consume();
            }
            return (canShoot, TakeFromMag);
        }
        private void Awaiting() => pending = true;
        private void Resolve()
        {
            delta = 0;
            pending = false;
        }
        private void Play(Func<SO_WeaponSFX, AudioClip> cb) => WeaponSettings.SFX.Play(cb);
        (bool down, bool up, bool hold, bool any) InputShoot()
        {
            var down = Input.GetButtonDown("Fire1");
            var up = Input.GetButtonUp("Fire1");
            var hold = Input.GetMouseButton(0);
            var any = down || hold;
            return (down, up, hold, any);
        }
    }
}
