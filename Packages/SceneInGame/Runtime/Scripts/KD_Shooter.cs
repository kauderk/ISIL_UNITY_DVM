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

        protected override void MyAwake()
        {
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        protected override void MyUpdate()
        {
            if (InputStoppedShooting())
            {
                Play(S => S.Release);
                transform.NotifySiblings<IFireEvent>(I => I.OnStopFire());
            }
            if (!InputIsShooting())
                return;

            clockRate += Time.deltaTime; // update the clock

            if (!IShooter.CanFire(clockRate))
                return;

            if (IShooter.Type == WeaponType.automatic)
                clockRate = 0f; // reset timer

            IShooter.Fire(OnBurst: () => Play(S => S.Burst));
            Play(S => S.Fire);

            transform.NotifySiblings<IFireEvent>(I => I.OnFire());
        }

        private void Play(Func<SO_WeaponSFX, AudioClip> cb) => WeaponSettings.SFX.Play(cb);
        bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        bool InputStoppedShooting() => Input.GetButtonUp("Fire1");
    }
}
