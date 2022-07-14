using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_EDITOR
[CustomEditor(typeof(KD_SkinMono))]
public class KD_SkinMonoEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (KD_SkinMono)target;

        if (GUILayout.Button("Change Targets"))
        {
            script.NotifySiblings();
        }
    }
}
#endif
