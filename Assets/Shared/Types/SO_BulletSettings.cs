using UnityEngine;
using System.Text.RegularExpressions;
using RotaryHeart.Lib.SerializableDictionary;
#if UNITY_EDITOR
using UnityEditor;
#endif

[System.Serializable]
public class UJigleDictionary<Tkey, Tval> : SerializableDictionaryBase<Tkey, Tval> { }


[CreateAssetMenu(fileName = "BulletSettings", menuName = "Game Data/BulletSettings", order = 1)]
public class SO_BulletSettings : ScriptableObject
{
    /// <summary>
    /// How fast will the bullet move.
    /// </summary>
    [field: SerializeField, Tooltip("How fast will the bullet move.")]
    public float speed { get; private set; } = 10f;

    /// <summary>
    /// How Far away until the bullet gets destroyed.
    /// </summary>
    [field: SerializeField, Tooltip("How Far away until the bullet gets destroyed.")]
    public float limitDistance { get; private set; } = 40f;

    /// <summary>
    /// Will target GameObject that include the Interface KD_IDamage
    /// </summary>
    [field: SerializeField, Tooltip("Will target GameObject that include the Interface KD_IDamage")]
    public float damage { get; private set; } = 5f;

    /// <summary>
    /// How much ammo will be consumed.
    /// </summary>
    [field: SerializeField, Tooltip("How much ammo will be consumed.")]
    public int magazineSize { get; private set; } = 10;

    /// <summary>
    /// When a collider triggers it's pickup function, the global UI color should change.
    /// </summary>
    [field: SerializeField, Tooltip("When a collider triggers it's pickup function, the global UI color should change.")]
    public Color color { get; private set; } = Color.white;

    /// <summary>
    /// It's material should be updated during Editor.
    /// </summary>
    [field: SerializeField, Tooltip("It's material should be updated during Editor.")]
    public MeshRenderer meshRenderer { get; set; }

    /// <summary>
    /// Using Shared Types: PISTOL, SHOTGUN, RIFLE
    /// </summary>
    [field: SerializeField, Tooltip("Using Shared Types: PISTOL, SHOTGUN, RIFLE")]
    public TYPEWEAPON weapon { get; private set; } = TYPEWEAPON.PISTOL;

    /// <summary>
    /// Move Behavior, shotting in straight line, with random jiggle.
    /// </summary>
    [field: SerializeField, Tooltip("Move Behavior, shotting in straight line, with random jiggle.")]
    public Vector2 jigle { get; private set; } = new Vector2(-0.3f, 0.3f);

    /// <summary>
    /// GameObject that shoots this bullet.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("GameObject that shoots this bullet.")]
    public GameObject caster { get; private set; }

    /// <summary>
    /// The scope of the bullet.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("The scope of the bullet.")]
    public Transform origin { get; private set; }

    [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
    public Vector3 originForward { get; private set; } = Vector3.zero;

    public SO_BulletSettings Init(GameObject caster, Transform origin, TYPEWEAPON weapon)
    {
        this.caster = caster;
        this.origin = origin;
        this.originForward = origin.forward;
        this.weapon = weapon;
        return this;
    }
    public static SO_BulletSettings Instantiate(SO_BulletSettings settings)
    {
        var @new = ScriptableObject.Instantiate(settings);
        return @new.Init(settings.caster, settings.origin, settings.weapon);
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