using UnityEngine;

namespace Weapon
{
    public class KD_Reloader : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponReloader Settings;

        KD_IWeaponReloader Iweapon;
        float delta;
        bool busy;

        private void Awake()
        {
            Iweapon = Settings;
            Iweapon.Init(WeaponSettings.Magazine, WeaponSettings.SFX);
        }

        void Update()
        {
            delta += Time.deltaTime;

            if (InputReload() && !busy)
            {
                busy = true;
                if (delta > Iweapon.reloadTime)
                {
                    Iweapon.FillMagazine();
                    delta = 0;
                    busy = false;
                }
            }
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

        bool InputReload() => Input.GetKeyDown(KeyCode.R);
    }
}