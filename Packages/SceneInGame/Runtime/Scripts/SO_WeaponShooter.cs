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
            var @new = InstanciateAmmo(AmmoSettings);

            var controller = @new.bullet.GetComponent<BulletController>();
            controller.enabled = false;
            controller.Init(@new.settings);
            controller.enabled = true;

            OnBurst?.Invoke();
        }

        private (GameObject bullet, SO_AmmoSettings settings) InstanciateAmmo(SO_AmmoSettings crr)
        {
            var bullet = Instantiate(crr.Bullet);
            var settings = Instantiate(crr);
            settings.Init(bullet, EditorSettings.Caster, EditorSettings.Scope);
            return (bullet, settings);
        }

        public void Init(SO_AmmoSettings Ammo = null, SOC_WeaponShooter Editor = null, SO_WeaponSkin skin = null)
        {
            AmmoSettings = Ammo ?? AmmoSettings;
            EditorSettings = Editor ?? EditorSettings;
            SkinSettings = skin ?? SkinSettings;
        }
    }
}