using EventBusSystem;
using UnityEngine;

namespace Weapon
{
    public class KD_Reloader : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Must Inherit from WeaponSettings")]
        public SO_WeaponReloader Settings;

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
                EventBus.RaiseEvent<IUIShootEvents>(I => I.OnMagazineChange(Stats, Magazine));
            }
        }
    }
}