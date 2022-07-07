#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SO_SceneLoader))]
public class SO_SceneLoaderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (SO_SceneLoader)target;

        // add white space
        EditorGUILayout.Space();

        if (GUILayout.Button("Play Preview", GUILayout.Height(40)))
        {
            script.ToogleFader(true);
        }
        if (GUILayout.Button("Stop Preview", GUILayout.Height(40)))
        {
            script.ToogleFader(false);
        }
    }
}
#endif