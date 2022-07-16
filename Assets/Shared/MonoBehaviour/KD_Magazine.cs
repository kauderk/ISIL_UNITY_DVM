using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Weapon
{
    public class KD_Magazine : MonoBehaviour, IMagazine
    {
        [field: SerializeField]
        public SO_AmmoSettings Settings { get; private set; }
        public void PickUp() { }

        public void updateMeshRenderer()
        {
            // thow not implemented exception
            throw new System.NotImplementedException();
            // if (!settings) return;
            // var meshRenderer = settings.Instance.Meshrenderer = GetComponent<MeshRenderer>();
            // // http://answers.unity.com/answers/322397/view.html
            // var tempMaterial = new Material(meshRenderer.sharedMaterial);
            // tempMaterial.color = settings.color;
            // meshRenderer.sharedMaterial = tempMaterial;
        }

        private void OnTriggerEnter(Collider other)
        {
            throw new System.NotImplementedException();
            //GlobalEvents.Invoke(IDs.pickupMagazine, settings.color);
        }
    }

#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(KD_Magazine))]
    public class KD_MagazineEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Update Material with Settings"))
            {
                var script = (KD_Magazine)target;
                script.updateMeshRenderer();
            }
        }
    }
#endif
}
