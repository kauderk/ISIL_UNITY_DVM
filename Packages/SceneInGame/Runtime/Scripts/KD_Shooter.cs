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
        float clockRate;
        SO_WeaponMagazine mag;

        protected override void MyAwake()
        {
            mag = Settings.Magazine;
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        protected override void MyUpdate()
        {
            var input = InputShoot();

            if (mag.Busy)
                return;

            if (input.up)
            {
                Play(S => S.Release);
                transform.NotifySiblings<IFireEvent>(I => I.OnStopFire());
            }
            clockRate += Time.deltaTime; // update the clock

            if (!input.down || !input.hold)
                return;

            if (input.down && !mag.hasAmmo())
            {
                Play(S => S.Empty);
                //return;
            }
            if (!IShooter.Elapsed(clockRate) || !mag.hasAmmo())
            {
                //Play(S => S.Dry);
                return;
            }

            if (IShooter.Type == WeaponType.automatic)
                clockRate = 0f; // reset timer

            IShooter.Fire(OnBurst: () => Play(S => S.Burst));
            Play(S => S.Fire);

            transform.NotifySiblings<IFireEvent>(I => I.OnFire());
        }

        private void Play(Func<SO_WeaponSFX, AudioClip> cb) => WeaponSettings.SFX.Play(cb);
        bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        bool InputStoppedShooting() => Input.GetButtonUp("Fire1");
        (bool down, bool up, bool hold) InputShoot()
        {
            var down = Input.GetButtonDown("Fire1");
            var up = Input.GetButtonUp("Fire1");
            var hold = Input.GetMouseButton(0);
            return (down, up, hold);
        }
    }
}
