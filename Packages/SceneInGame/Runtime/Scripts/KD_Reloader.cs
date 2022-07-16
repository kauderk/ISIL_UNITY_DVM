using System;
using UnityEngine;

namespace Weapon
{

    public class KD_Reloader : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
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
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IMagazine>(out var magazine))
            {
                collision.gameObject.SetActive(false);
                //Debug.Break();
                //SO_RedirectSignalManager.Signal.PickUp(gameObject);
                SO_RedirectSignalManager.UseUpwardSignal<KD_ISignalListener>(gameObject);
                SO_RedirectSignalManager.UseLateralSignal<KD_ISignalListener>(gameObject);
                //var settings = magazine.PickUp();
                //Iweapon.FillMagazine();
                // TODO: Fire a Gloabl Event to update the UI 
                //txtBulletCount.color = settings.color;
            }
        }
    }
}