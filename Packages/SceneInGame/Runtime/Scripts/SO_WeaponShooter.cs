using UnityEngine;
using Store;
using System;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponShooter", menuName = "Game Data/WeaponShooter")]
    public class SO_WeaponShooter : ScriptableObject, IWeaponShooter, IBaseWeaponCmp
    {
        [field: SerializeField]
        public WeaponType Type { get; private set; }

        [field: SerializeField]
        public int FireRate { get; private set; }

        [field: SerializeField]
        public float Cadence { get; private set; }

        SO_WeaponMagazine magazine;

        GameObject Bullet;
        SOC_WeaponShooter EditorSettings;

        public bool Elapsed(float deltaFireRate) => deltaFireRate > Cadence;

        public void Fire(Action OnBurst = null)
        {
            for (int i = 0; i < FireRate; i++)
            {
                var bullet = Instantiate(this.Bullet);
                var controller = bullet.GetComponent<BulletController>();
                controller.enabled = false;

                var AmmoRef = Store.SO_Artillery.Instance.Ammo[magazine.Type]; // the only "dependency", how do you avoid this?
                var bulletSettings = Instantiate(AmmoRef);

                OnBurst?.Invoke();
                bulletSettings.Init(bullet, EditorSettings.Caster, EditorSettings.Scope);
                controller.Init(bulletSettings);

                controller.enabled = true;
            }
        }

        public void Init(SO_WeaponMagazine Magazine, SO_AmmoSettings BulletSettings, SOC_WeaponShooter EditorSettings)
        {
            this.magazine = Magazine;
            this.Bullet = BulletSettings.Bullet;
            this.EditorSettings = EditorSettings;
        }
    }
}