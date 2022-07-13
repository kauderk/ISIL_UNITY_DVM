using UnityEngine;
using UnityEditor;


/// <summary>
/// Use this property on a ScriptableObject type to allow the editors drawing the field to draw an expandable
/// area that allows for changing the values on the object without having to change editor.
/// </summary>
public class NonExpandableAttribute : PropertyAttribute
{
    public NonExpandableAttribute() { }
}
[CustomPropertyDrawer(typeof(NonExpandableAttribute), true)]
public class NonExpandableAttributeDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // override the default
        EditorGUI.PropertyField(position, property, label, false);
    }
}
// https://forum.unity.com/threads/editor-tool-better-scriptableobject-inspector-editing.484393/
[CustomPropertyDrawer(typeof(ScriptableObject), true)]
public class ScriptableObjectDrawer : PropertyDrawer
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