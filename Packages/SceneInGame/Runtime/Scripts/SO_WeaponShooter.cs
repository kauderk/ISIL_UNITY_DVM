using UnityEngine;
using Cysharp.Threading.Tasks;

using Store;
using System;
using System.Collections;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponShooter", menuName = "Game Data/WeaponShooter")]
    public class SO_WeaponShooter : ScriptableObject, IWeaponShooter, IBaseWeaponCmp
    {
        [field: SerializeField]
        public WeaponType Type { get; private set; }

        [field: SerializeField]
        public int Burst { get; private set; } = 1;

        [field: SerializeField]
        public float Cadence { get; private set; } = .3f;

        [field: SerializeField]
        public float Delay { get; private set; } = 0;

        SO_WeaponMagazine magazine;
        GameObject Bullet;
        SOC_WeaponShooter EditorSettings;

        public bool Elapsed(float deltaFireRate) => deltaFireRate > Cadence;

        public async void Fire(Action OnBurst = null, float delayInSeconds = 0)
        {
            for (int i = 0; i < Burst; i++)
            {
                if (delayInSeconds == 0)
                    CreateBurst(OnBurst);
                else
                    await DelayedBurst(OnBurst, delayInSeconds);
            }
        }
        async UniTask DelayedBurst(Action OnBurst, float delayInSeconds)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delayInSeconds));
            CreateBurst(OnBurst);
        }
        private void CreateBurst(Action OnBurst)
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
        public void Init(SO_WeaponMagazine Magazine, SO_AmmoSettings BulletSettings, SOC_WeaponShooter EditorSettings)
        {
            this.magazine = Magazine;
            this.Bullet = BulletSettings.Bullet;
            this.EditorSettings = EditorSettings;
        }
    }
}