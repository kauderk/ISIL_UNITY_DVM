using UnityEngine;
using Photon.Pun;

namespace Weapon
{
    public class KD_Shooter : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;

        IWeaponShooter IShooter;
        float clockRate;

        void Awake()
        {
            if (!photonView.IsMine)
                return;
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        protected override void MyUpdate()
        {
            if (InputIsShooting() && IShooter.CanFire(clockRate))
            {
                if (IShooter.Type == WeaponType.automatic)
                    clockRate = 0f; // reset timer

                IShooter.Fire();

                transform.NotifySiblings<IFireEvent>(I => I.OnFire());
                Debug.Log($"Is mine: {photonView.IsMine} with Amunition: {WeaponSettings.Magazine.amoution}");
            }
            else if (InputIsShooting())
            {
                clockRate += Time.deltaTime;
            }

            if (InputStoppedShooting())
            {
                transform.NotifySiblings<IFireEvent>(I => I.OnStopFire());
            }
        }

        bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
        bool InputStoppedShooting() => Input.GetButtonUp("Fire1");
    }
}
