using UnityEngine;
using System.Text.RegularExpressions;
#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Game Data/BulletSettings", order = 1)]
public class SO_BulletSettings : ScriptableObject
{
    /// <summary>
    /// How fast the bullet will move.
    /// </summary>
    [field: SerializeField, UsePropertyName]
    public float speed { get; } = 30f;
    /// <summary>
    /// How Far away until the bullet gets destroyed.
    /// </summary>
    [field: SerializeField, UsePropertyName]
    public float limitDistance { get; } = 20f;
    /// <summary>
    /// Will target GameObject that include the Interface KD_IDamage
    /// </summary>
    [field: SerializeField, UsePropertyName]
    public float damage { get; } = 10f;
    /// <summary>
    /// Using Shared Types: PISTOL, SHOTGUN, RIFLE
    /// </summary>
    [field: SerializeField, UsePropertyName]
    public TYPEWEAPON weapon { get; } = TYPEWEAPON.PISTOL;
    /// <summary>
    /// GameObject that shot this bullet.
    /// </summary>
    public GameObject caster { get; }

    public SO_BulletSettings(GameObject caster, Vector3 origin, TYPEWEAPON weapon)
    {
        this.caster = caster;
        this.originPos = origin;
        this.weapon = weapon;
    }

    /// <summary>
    /// The scope of the bullet.
    /// </summary>
    public Transform origin { get; }
    public Vector3 originPos { get; } = Vector3.zero;
}
#if UNITY_EDITOR
/// <summary>
/// Use this attribute in combination with a [SerializeField] attribute on top of a property to display the property name. Example:
/// [field: SerializeField, UsePropertyName]
/// public int number { get; private set; }
/// </summary>
public class UsePropertyNameAttribute : PropertyAttribute { }
[CustomPropertyDrawer(typeof(UsePropertyNameAttribute))]
public class UsePropertyNameDrawer : PropertyDrawer
{
    private const string Pattern = @"<(.+)>k__Backing Field";

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var match = Regex.Match(label.text, Pattern);
        if (match.Success && match.Groups.Count == 2 && match.Groups[1].Captures.Count == 1)
        {
            var newLabel = new GUIContent(match.Groups[1].Captures[0].Value, label.tooltip);
            EditorGUI.PropertyField(position, property, newLabel);
        }
        else
        {
            EditorGUI.PropertyField(position, property, label);
        }
    }
}
#endif