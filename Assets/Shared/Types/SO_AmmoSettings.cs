using UnityEngine;

[CreateAssetMenu(fileName = "AmmoSettings", menuName = "Game Data/SO_AmmoSettings")]
public class SO_AmmoSettings : ScriptableObject
{
    [field: SerializeField, Tooltip("How fast will the bullet move.")]
    public GameObject Bullet { get; private set; }


    [field: SerializeField, Tooltip("How fast will the bullet move.")]
    public float speed { get; private set; } = 10f;


    [field: SerializeField, Tooltip("How Far away until the bullet gets destroyed.")]
    public float limitDistance { get; private set; } = 40f;


    [field: SerializeField, Tooltip("Will target GameObject that include the Interface KD_IDamage")]
    public float damage { get; private set; } = 5f;


    [field: SerializeField, Tooltip("When a collider triggers it's pickup function, the global UI color should change.")]
    public Color color { get; private set; } = Color.white;


    [field: SerializeField, Tooltip("Move Behavior, shotting in straight line, with random jiggle.")]
    public Vector2 jigle { get; private set; } = new Vector2(-0.3f, 0.3f);


    [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
    public Vector3 originForward { get; private set; } = Vector3.zero;

    public SOC_BulletInstance Instance;

    public SO_AmmoSettings Init(GameObject bullet, GameObject caster, Transform origin)
    {
        this.Instance.bullet = bullet;
        this.Instance.caster = caster;
        this.Instance.origin = origin;
        this.originForward = origin.forward;
        return this;
    }
    public static SO_AmmoSettings Instantiate(SO_AmmoSettings settings)
    {
        var @new = ScriptableObject.Instantiate(settings);
        return @new.Init(settings.Instance.bullet, settings.Instance.caster, settings.Instance.origin);
    }
}

[System.Serializable]
public class SOC_BulletInstance
{

    [field: SerializeField, ReadOnly, Tooltip("GameObject that shoots this bullet.")]
    public GameObject caster { get; set; }


    [field: SerializeField, ReadOnly, Tooltip("The actual Bullet Model.")]
    public GameObject bullet { get; set; }


    [field: SerializeField, ReadOnly, Tooltip("It's material should be updated during Editor.")]
    public MeshRenderer meshRenderer { get; set; }


    [field: SerializeField, ReadOnly, Tooltip("The scope of the bullet.")]
    public Transform origin { get; set; }


    [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
    public Vector3 originForward { get; set; } = Vector3.zero;
}
