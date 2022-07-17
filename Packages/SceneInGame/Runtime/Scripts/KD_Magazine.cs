using UnityEngine;

namespace Weapon
{
    public class KD_Magazine : WeaponMonoBehaviourPunBase, IMagazine
    {
        [field: SerializeField, HideInInspector, Tooltip("Inherited from WeaponSettings")]
        public SO_WeaponMagazine Settings;

        [field: SerializeField, HideInInspector, Tooltip("Inherited from WeaponSettings")]
        public SO_WeaponSkin SkinSettings;

        public SO_WeaponMagazine settings { get { return Settings; } }

        public SO_WeaponSkin skinSettings { get { return SkinSettings; } }

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
