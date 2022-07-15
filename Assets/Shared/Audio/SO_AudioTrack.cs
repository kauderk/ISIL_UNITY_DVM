using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "AudioTrack", menuName = "Audio/AudioTrack")]
public class SO_AudioTrack : ScriptableObject
{
    [Tooltip("Price to unlock this sound"), ReadOnly]
    public float Threshold = 0.0f;
    public AudioClip Clip;

    [HideInInspector]
    public TrackInstances instances = new TrackInstances();
    [HideInInspector]
    public Other Other = new Other();
}
// public class HorizontalAttribute : PropertyAttribute
// {
//     public string Name;
//     public HorizontalAttribute(string name) => Name = name;
// }

// [CustomPropertyDrawer(typeof(HorizontalAttribute))]
// public class HorizontalDrawer : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         var horizontal = attribute as HorizontalAttribute;

//         if (horizontal.Name == "Begin")
//         {
//             EditorGUILayout.EndHorizontal(); // WTF!
//             EditorGUILayout.BeginHorizontal(); // WTF!
//             NewMethod(position, property, label);
//         }
//         else if (horizontal.Name == "End")
//         {
//             EditorGUILayout.EndHorizontal(); // WTF!
//             EditorGUILayout.BeginHorizontal(); // WTF!
//             NewMethod(position, property, label);
//         }
//         else
//         {
//             NewMethod(position, property, label);
//         }
//     }

//     private static void NewMethod(Rect position, SerializedProperty property, GUIContent label)
//     {
//         EditorGUIUtility.labelWidth = EditorGUIUtility.fieldWidth = 50;
//         EditorGUI.PropertyField(position, property, label);
//     }
// }
