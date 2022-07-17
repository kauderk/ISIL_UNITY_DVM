using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

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

        SO_AmmoSettings AmmoSettings;
        SOC_WeaponShooter EditorSettings;
        SO_WeaponSkin SkinSettings;

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
            var bullet = Instantiate(AmmoSettings.Bullet);
            var controller = bullet.GetComponent<BulletController>();
            controller.enabled = false;

            var Ammo = Instantiate(AmmoSettings);
            OnBurst?.Invoke();
            Ammo.Init(bullet, EditorSettings.Caster, EditorSettings.Scope);
            controller.Init(Ammo);

            controller.enabled = true;
        }
        public void Init(SO_AmmoSettings Ammo = null, SOC_WeaponShooter Editor = null, SO_WeaponSkin skin = null)
        {
            AmmoSettings = Ammo ?? AmmoSettings;
            EditorSettings = Editor ?? EditorSettings;
            SkinSettings = skin ?? SkinSettings;
        }
    }
}