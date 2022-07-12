using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawerF : PropertyDrawer
{
    // Cached scriptable object editor
    private Editor editor = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Draw label
        EditorGUI.PropertyField(position, property, label, true);

        if (property.objectReferenceValue == null)
            return; // draw it's picker

        // Draw foldout arrow
        property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none);

        // Draw foldout properties
        if (property.isExpanded)
        {
            // Make child fields be indented
            EditorGUI.indentLevel++;

            // Draw object properties
            if (!editor)
                Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);
            editor.OnInspectorGUI();

            // Set indent back to what it was
            EditorGUI.indentLevel--;
        }
    }
}