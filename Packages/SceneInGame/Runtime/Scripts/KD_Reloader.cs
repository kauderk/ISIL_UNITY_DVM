using EventBusSystem;
using UnityEngine;

namespace Weapon
{
    public class KD_Reloader : KD_MonoWeapon, ICollisionSubscriber
    {
        [SerializeField, HideInInspector, Tooltip("Must Inherit from WeaponSettings")]
        public SO_WeaponReloader Settings;

        public void OnCollisionWithMagazine(SO_WeaponSettings weaponSettings, Collision collision)
        {
            WeaponSettings = weaponSettings;
            UpdateMagazine();
            SetUp();
        }

        private void SetUp()
        {
            Resolve(BusyMagazine: false);
            Settings = WeaponSettings.Reloader;
        }

        protected override void MyUpdate()
        {
            var input = this.InputReload();

            if (pending)
                delta += Time.deltaTime;
            if (pending && input.any)
                Play(S => S.BusyReloading);

            if (!pending && input.fully)
            {
                Awaiting(BusyMagazine: true);
                Play(S => S.FullyReloaded);
                WeaponSettings.Magazine.Fill();
            }
            if (delta > WeaponSettings.SFX.FullyReloaded.length)
            {
                Resolve(BusyMagazine: false);
                EventBus.RaiseEvent<IUIShootEvents>(I => I.OnMagazineChange(Stats, WeaponSettings.Magazine));
            }
        }
    }
}