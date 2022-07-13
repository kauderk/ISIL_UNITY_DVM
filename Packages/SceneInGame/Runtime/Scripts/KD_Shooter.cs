using UnityEngine;
using Photon.Pun;

namespace Weapon
{
    public class KD_Shooter : MonoBehaviourPunCallbacks
    {
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;

        IWeaponShooter IShooter;
        float deltaFireRate = 0f;

        void Awake()
        {
            IShooter = Settings;
            IShooter.Init(EditorSettings);
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