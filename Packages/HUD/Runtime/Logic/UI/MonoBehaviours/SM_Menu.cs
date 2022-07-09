using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwipeMenu;
using UnityEditor;
using System.Reflection;
using System;
using System.Linq;
using UnityEngine.Events;

public class SM_Menu : SwipeMenu.Menu
{
}
#if UNITY_EDITOR
[CustomEditor(typeof(SM_Menu))]
public class SM_MenuEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var script = (SM_Menu)target;

        // add white space
        EditorGUILayout.Space();

        if (GUILayout.Button("Awake", GUILayout.Height(40)))
        {
            script.Awake();
        }
    }
}
#endif

// [CustomEditor(typeof(SM_Menu))]
// public class CameraMovementEditor : Editor
// {
//     private List<SerializedProperty> properties;

//     private void OnEnable()
//     {
//         string[] hiddenProperties = new string[] { "menuItems" }; //fields you do not want to show go here
//         properties = EditorHelper.GetExposedProperties(this.serializedObject, hiddenProperties);
//     }

//     public override void OnInspectorGUI()
//     {
//         if (Application.isPlaying)
//         {
//             base.OnInspectorGUI(); //; - (standard way to draw base inspector)
//             return;
//         }

//         //We draw only the properties we want to display here
//         foreach (SerializedProperty property in properties)
//         {
//             EditorGUILayout.PropertyField(property, true);
//         }
//         serializedObject.ApplyModifiedProperties();
//     }
// }
// public static class EditorHelper
// {
//     public static List<SerializedProperty> GetExposedProperties(SerializedObject so, IEnumerable<string> namesToHide = null)
//     {
//         if (namesToHide == null) namesToHide = new string[] { };
//         IEnumerable<FieldInfo> componentFields = so.targetObject.GetType().GetFields();
//         List<SerializedProperty> exposedFields = new List<SerializedProperty>();

//         foreach (FieldInfo info in componentFields)
//         {
//             bool displayInInspector = info.IsPublic && !Attribute.IsDefined(info, typeof(HideInInspector));
//             displayInInspector = displayInInspector || (info.IsPrivate && Attribute.IsDefined(info, typeof(SerializeField)));
//             displayInInspector = displayInInspector && !namesToHide.Contains(info.Name);

//             if (displayInInspector)
//             {
//                 SerializedProperty prop = so.FindProperty(info.Name);
//                 if (prop != null)
//                     exposedFields.Add(prop);
//             }
//         }

//         return exposedFields;
//     }
// }