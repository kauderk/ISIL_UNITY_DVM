using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class KD_Magazine : MonoBehaviour, KD_IMagazine
{
    [field: SerializeField]
    public SO_BulletSettings settings { get; private set; }
    public SO_BulletSettings PickUp() => settings;


    public void updateMeshRenderer()
    {
        if (!settings) return;
        var meshRenderer = settings.Instance.meshRenderer = GetComponent<MeshRenderer>();
        // http://answers.unity.com/answers/322397/view.html
        var tempMaterial = new Material(meshRenderer.sharedMaterial);
        tempMaterial.color = settings.color;
        meshRenderer.sharedMaterial = tempMaterial;
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
