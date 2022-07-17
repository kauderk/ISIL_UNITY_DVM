using System;
using EventBusSystem;
using UnityEngine;

namespace Weapon
{
    public class KD_Shooter : KD_MonoWeapon, ICollisionSubscriber
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from WeaponSettings")]
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;

        IWeaponShooter IShooter;

        private void OnEnable() => EventBus.Subscribe(this);
        private void OnDisable() => EventBus.Unsubscribe(this);

        public void OnCollisionWithMagazine(Collision collision)
        {
            var magazine = collision.gameObject.GetComponent<IMagazine>();
            var Ammo = Store.SO_Artillery.Instance.Ammo[magazine.settings.Type];
            IShooter.Init(Ammo: Ammo, skin: magazine.skinSettings);
        }

        protected override void MyAwake()
        {
            base.MyAwake();
            delta = Settings.Burst + 1; // able to shoot on start up
            IShooter = WeaponSettings.Shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Ammo, EditorSettings, WeaponSettings.Skin);
        }

        protected override void MyUpdate() // Use a State Machine FIXME:
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
                IShooter.Fire(OnBurst: () => Play(S => S.Burst), IShooter.Delay);
                Play(S => S.Fire);

                transform.NotifySiblings<IFireEvent>(I => I.OnFire());
            }
        }
    }
}
