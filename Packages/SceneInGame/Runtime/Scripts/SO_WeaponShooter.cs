using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponShooter", menuName = "Game Data/WeaponShooter")]
    public class SO_WeaponShooter : ScriptableObject, IWeaponShooter
    {
        [field: SerializeField]
        public WeaponType Type { get; private set; }

        [field: SerializeField]
        public int FireRate { get; private set; }

        [field: SerializeField]
        public float Cadence { get; private set; }

        public SO_WeaponMagazine Magazine { get; private set; }


        GameObject Bullet;
        SOC_WeaponShooter EditorSettings;

        public bool CanFire(float deltaFireRate)
        {
            bool enoughTime = deltaFireRate > Cadence;
            bool hasAmmo = Magazine.amoution > 0;
            return hasAmmo && enoughTime;
        }

        public void Fire()
        {
            Magazine.consume();
            for (int i = 0; i < FireRate; i++)
            {
                var bullet = Instantiate(this.Bullet);
                var controller = bullet.GetComponent<BulletController>();
                controller.enabled = false;

                var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

                bulletSettings.Init(bullet, EditorSettings.Caster, EditorSettings.Scope);
                controller.Init(bulletSettings);

                controller.enabled = true;
            }
        }

        public void Init(SO_WeaponMagazine Magazine, SO_BulletSettings BulletSettings, SOC_WeaponShooter EditorSettings)
        {
            this.EditorSettings = EditorSettings;
            this.Magazine = Magazine;
            this.Bullet = BulletSettings.Bullet;
        }
    }
}