using UnityEngine;

namespace Weapon
{
    public class KD_Shooter : KD_MonoWeapon
    {
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;

        IWeaponShooter IShooter;
        float deltaFireRate;

        void Awake()
        {
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        void Update()
        {
            if (InputIsShooting() && IShooter.CanFire(deltaFireRate))
            {
                if (IShooter.Type == WeaponType.automatic)
                    deltaFireRate = 0f; // reset timer

                IShooter.Fire();
            }
            else if (InputIsShooting())
            {
                deltaFireRate += Time.deltaTime;
            }
        }

        bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }
}
