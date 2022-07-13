#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

// https://www.anton.website/colored-inspector-fields/
public static class EditorUtils
{
    public static T AssignField<T>(T other, SerializedProperty field, string otherField = "child field") where T : Object
    {
        if (other != null)
        {
            //Object obj = EditorGUILayout.ObjectField("Settings", (Object)other, typeof(SO_WeaponShooter), false);
            EditorGUILayout.PropertyField(field);
            return other;
        }
        else
        {
            ErrorInspector(otherField);
            return null;
        }
    }

    public static void ErrorInspector(string otherField)
    {
        EditorGUILayout.HelpBox($"{otherField} is null.", MessageType.Error);
    }
}
#endif
