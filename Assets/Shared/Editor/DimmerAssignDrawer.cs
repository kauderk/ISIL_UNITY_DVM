using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DimmerAssign : PropertyAttribute
{
    public Color color;

    public DimmerAssign(Color _color) => color = _color;
    public DimmerAssign() => color = new Color(1, 1, 1, 0.5f); // semi transparent color
}
#if UNITY_EDITOR
// https://www.anton.website/colored-inspector-fields/
[CustomPropertyDrawer(typeof(DimmerAssign))]
public class DimmerAssignDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var field = attribute as DimmerAssign;

        if (property.objectReferenceValue != null)
        {
            // semi transparent color
            GUI.color = field.color; //Set the color of the GUI
            EditorGUI.PropertyField(position, property, label); //Draw the GUI
            GUI.color = Color.white; //Reset the color of the GUI to white
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif