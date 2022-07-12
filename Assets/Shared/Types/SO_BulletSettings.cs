using UnityEngine;

[CreateAssetMenu(fileName = "BulletSettings", menuName = "Game Data/BulletSettings", order = 1)]
public class SO_BulletSettings : ScriptableObject
{
    #region 
    /// <summary>
    /// How fast will the bullet move.
    /// </summary>
    [field: SerializeField, Tooltip("How fast will the bullet move.")]
    #endregion
    public float speed { get; private set; } = 10f;

    #region 
    /// <summary>
    /// How Far away until the bullet gets destroyed.
    /// </summary>
    [field: SerializeField, Tooltip("How Far away until the bullet gets destroyed.")]
    #endregion
    public float limitDistance { get; private set; } = 40f;

    #region 
    /// <summary>
    /// Will target GameObject that include the Interface KD_IDamage
    /// </summary>
    [field: SerializeField, Tooltip("Will target GameObject that include the Interface KD_IDamage")]
    #endregion
    public float damage { get; private set; } = 5f;

    #region 
    /// <summary>
    /// How much ammo will be consumed.
    /// </summary>
    [field: SerializeField, Tooltip("How much ammo will be consumed.")]
    #endregion
    public int magazineSize { get; private set; } = 10;

    #region 
    /// <summary>
    /// When a collider triggers it's pickup function, the global UI color should change.
    /// </summary>
    [field: SerializeField, Tooltip("When a collider triggers it's pickup function, the global UI color should change.")]
    #endregion
    public Color color { get; private set; } = Color.white;

    #region 
    /// <summary>
    /// Using Shared Types: PISTOL, SHOTGUN, RIFLE
    /// </summary>
    [field: SerializeField, Tooltip("Using Shared Types: PISTOL, SHOTGUN, RIFLE")]
    #endregion
    public TYPEWEAPON weapon { get; private set; } = TYPEWEAPON.PISTOL;

    #region 
    /// <summary>
    /// Move Behavior, shotting in straight line, with random jiggle.
    /// </summary>
    [field: SerializeField, Tooltip("Move Behavior, shotting in straight line, with random jiggle.")]
    #endregion
    public Vector2 jigle { get; private set; } = new Vector2(-0.3f, 0.3f);

    #region 
    /// <summary>
    /// GameObject that shoots this bullet.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("GameObject that shoots this bullet.")]
    #endregion
    public GameObject caster { get; private set; }

    #region 
    /// <summary>
    /// The actual Bullet Model.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("The actual Bullet Model.")]
    #endregion
    public GameObject bullet { get; private set; }

    #region 
    /// <summary>
    /// It's material should be updated during Editor.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("It's material should be updated during Editor.")]
    #endregion
    public MeshRenderer meshRenderer { get; set; }

    #region 
    /// <summary>
    /// The scope of the bullet.
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("The scope of the bullet.")]
    #endregion
    public Transform origin { get; private set; }

    #region
    /// <summary>
    /// Where the bullet is headed. Cached world position, based on origin's forward vector
    /// </summary>
    [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
    #endregion
    public Vector3 originForward { get; private set; } = Vector3.zero;

    public SO_BulletSettings Init(GameObject bullet, GameObject caster, Transform origin, TYPEWEAPON weapon)
    {
        this.bullet = bullet;
        this.caster = caster;
        this.origin = origin;
        this.originForward = origin.forward;
        this.weapon = weapon;
        return this;
    }
    public static SO_BulletSettings Instantiate(SO_BulletSettings settings)
    {
        var @new = ScriptableObject.Instantiate(settings);
        return @new.Init(settings.bullet, settings.caster, settings.origin, settings.weapon);
    }
}
