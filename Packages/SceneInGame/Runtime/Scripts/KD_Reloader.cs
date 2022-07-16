using System;
using UnityEngine;

namespace Weapon
{
    public class KD_Reloader : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponReloader Settings;

        KD_IWeaponReloader Iweapon;
        float delta;
        bool pending;

        private void Awake() => Iweapon = Settings;

        void Update()
        {
            var input = InputReload();

            if (pending)
                delta += Time.deltaTime;
            if (pending && input.any)
                Play(S => S.BusyReloading);

            if (!pending && input.fully)
            {
                awaiting();
                Play(S => S.FullyReloaded);
                WeaponSettings.Magazine.fill();
            }
            if (delta > WeaponSettings.SFX.FullyReloaded.length)
            {
                resolve();
            }
        }

        private void awaiting()
        {
            WeaponSettings.Magazine.Busy = true;
            pending = true;
        }

        private void resolve()
        {
            WeaponSettings.Magazine.Busy = false;
            delta = 0;
            pending = false;
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<KD_IMagazine>(out var magazine))
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

        private void Play(Func<SO_WeaponSFX, AudioClip> cb) => WeaponSettings.SFX.Play(cb);
        bool InputFullyReload() => Input.GetKeyDown(KeyCode.R);
        (bool fully, bool single, bool any) InputReload()
        {
            var fully = Input.GetKeyDown(KeyCode.R);
            var single = Input.GetKeyDown(KeyCode.T);
            var any = fully || single;
            return (fully, single, any);
        }
    }
}