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
    [field: SerializeField]
    public float speed { get; private set; } = 30f;

    /// <summary>
    /// How Far away until the bullet gets destroyed.
    /// </summary>
    [field: SerializeField]
    public float limitDistance { get; private set; } = 20f;

    /// <summary>
    /// Will target GameObject that include the Interface KD_IDamage
    /// </summary>
    [field: SerializeField]
    public float damage { get; private set; } = 10f;

    /// <summary>
    /// Using Shared Types: PISTOL, SHOTGUN, RIFLE
    /// </summary>
    [field: SerializeField]
    public TYPEWEAPON weapon { get; private set; } = TYPEWEAPON.PISTOL;

    /// <summary>
    /// GameObject that shot this bullet.
    /// </summary>
    public GameObject caster { get; private set; }

    /// <summary>
    /// The scope of the bullet.
    /// </summary>
    public Vector3 origin { get; private set; }
    public Vector3 forward { get; private set; } = Vector3.zero;

    public Vector3[] jiggle = new Vector3[2];

    [field: SerializeField]
    public LayerMask layerMask { get; private set; } = LayerMask.GetMask("Trees");

    public SO_BulletSettings(GameObject caster, Vector3 origin, TYPEWEAPON weapon)
    {
        Init(caster, origin, weapon);
    }
    public void Init(GameObject caster, Vector3 origin, TYPEWEAPON weapon)
    {
        this.caster = caster;
        this.origin = origin;
        this.forward = forward;
        this.weapon = weapon;
    }
    public void Init(SO_BulletSettings settings)
    {
        this.caster = settings.caster;
        this.origin = settings.origin;
        this.forward = settings.forward;
        this.weapon = settings.weapon;
    }
}
#if UNITY_EDITOR
/// <summary>
/// https://gist.github.com/FlaShG/49fcaf93bd8682a9722fd11950345b90 <br/>
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