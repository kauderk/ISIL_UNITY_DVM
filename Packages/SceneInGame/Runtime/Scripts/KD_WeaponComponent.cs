using UnityEngine;
using Weapon;

namespace Weapon
{
    public class KD_WeaponComponent : MonoBehaviour//, IWeaponShooter
    {
        public int MagazineSize { get; } = 10;
        public int Amoution { get; private set; } = 10;
        public int Reloadamount { get; } = 10;
        public float Reloadtime { get; } = 1f;
        public WeaponType Type { get; }
        public int FireRate { get; } = 1;
        public float Cadence { get; } = 0.1f;
        public bool Isreloading { get; private set; } = false;
        public TYPEWEAPON Weapon { get; }
        public Transform Scope { get; }
        public GameObject Bullet { get; }

        public bool CanFire(float deltaFireRate)
        {
            bool enoughTime = deltaFireRate > Cadence;
            bool hasAmmo = Amoution > 0;
            return hasAmmo && enoughTime;
        }

        public void Fire()
        {
            for (int i = 0; i < FireRate; i++)
            {
                var bullet = Instantiate(this.Bullet);
                var controller = bullet.GetComponent<BulletController>();
                controller.enabled = false;

                var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

                bulletSettings.Init(bullet, gameObject, Scope);
                controller.Init(bulletSettings);

                controller.enabled = true;
            }
        }


        public void FillMagazine() => Amoution = MagazineSize;

        public void Reload()
        {
            Isreloading = true;
            if (Amoution < MagazineSize)
                Amoution += Reloadamount;
        }

        public void Init(SOC_WeaponShooter EditorSettings)
        {
            throw new System.NotImplementedException();
        }

        public void Init(SOC_WeaponShooter EditorSettings, KD_IWeaponMagazine Magazine)
        {
            throw new System.NotImplementedException();
        }
    }
}