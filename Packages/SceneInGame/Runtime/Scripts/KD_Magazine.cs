using UnityEngine;

namespace Weapon
{
    [System.Serializable]
    public class KD_Magazine : WeaponMonoBehaviourPunBase, IMagazine
    {
        [field: SerializeField, HideInInspector, Tooltip("Must Inherit from WeaponSettings")]
        public SO_WeaponMagazine Settings { get; set; }

        [field: SerializeField, HideInInspector, Tooltip("Must Inherit from WeaponSettings")]
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
    }
}
