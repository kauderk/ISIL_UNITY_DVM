using UnityEngine;
using Visual;

namespace Weapon
{
    [System.Serializable]
    public class KD_Magazine : WeaponMonoBehaviourPunBase, IMagazine
    {
        [field: SerializeField, Tooltip("Must Inherit from WeaponSettings")]
        public SO_WeaponMagazine Settings { get; set; }

        [field: SerializeField, Tooltip("Must Inherit from WeaponSettings")]
        public SO_WeaponSkin SkinSettings { get; set; }

        private void OnEnable()
        {
            Settings = WeaponSettings.Magazine;
            SkinSettings = WeaponSettings.Skin;
        }

        public void PickUp() { }

        public void updateMeshRenderer()
        {
            if (!SkinSettings) return;
            var meshRenderer = GetComponent<MeshRenderer>();
            // http://answers.unity.com/answers/322397/view.html
            var tempMaterial = new Material(meshRenderer.sharedMaterial);
            tempMaterial.color = SkinSettings.Color.Runtime;
            meshRenderer.sharedMaterial = tempMaterial;
        }

        public (SO_AmmoSettings ammo, SO_WeaponSkin skin) AmmoAndVisualOnPickup(IMagazine magazine)
        {
            // we can do better that that FIXME:
            var ammo = Instantiate(Store.SO_Artillery.Instance.Ammo[magazine.Settings.Type]);
            var skin = Instantiate(magazine.SkinSettings);
            ammo.Init(ammo.Bullet);
            return (ammo, skin);
        }
    }
}
