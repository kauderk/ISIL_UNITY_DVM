using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "AmmoSettings", menuName = "Game Data/SO_AmmoSettings")]
    public class SO_AmmoSettings : ScriptableObject
    {
        [field: SerializeField, Tooltip("How fast will the bullet move.")]
        public GameObject Bullet { get; private set; }

        [field: SerializeField, Tooltip("How fast will the bullet move.")]
        public float Speed { get; private set; } = 10f;

        [field: SerializeField, Tooltip("How Far away until the bullet gets destroyed.")]
        public float DestroyAtDistance { get; private set; } = 40f;

        [field: SerializeField, Tooltip("Will target GameObject that include the Interface KD_IDamage")]
        public float Damage { get; private set; } = 5f;

        [field: SerializeField, Tooltip("Move Behavior, shotting in straight line, with random jiggle.")]
        public Vector2 Jigle { get; private set; } = new Vector2(-0.3f, 0.3f);

        [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
        public Vector3 OriginForward { get; private set; } = Vector3.zero;

        public SOC_BulletInstance Instance;

        public SO_AmmoSettings Init(GameObject bullet, GameObject caster, Transform origin)
        {
            Instance.Bullet = bullet;
            Instance.Caster = caster;
            Instance.Origin = origin;
            OriginForward = origin.forward;
            return this;
        }
        public SO_AmmoSettings Init(GameObject bullet)
        {
            Instance.Bullet = bullet;
            return this;
        }
        public static SO_AmmoSettings Init(SO_AmmoSettings settings)
        {
            // go fuck yourself rosynator, I DO need the Unity Version of Instantiate()
            var @new = ScriptableObject.Instantiate(settings);
            return @new.Init(settings.Instance.Bullet, settings.Instance.Caster, settings.Instance.Origin);
        }
    }

    [System.Serializable]
    public class SOC_BulletInstance
    {
        [field: SerializeField, ReadOnly, Tooltip("GameObject that shoots this bullet.")]
        public GameObject Caster { get; set; }

        [field: SerializeField, ReadOnly, Tooltip("The actual Bullet Model.")]
        public GameObject Bullet { get; set; }

        [field: SerializeField, ReadOnly, Tooltip("It's material should be updated during Editor.")]
        public MeshRenderer Meshrenderer { get; set; }

        [field: SerializeField, ReadOnly, Tooltip("The scope of the bullet.")]
        public Transform Origin { get; set; }

        [field: SerializeField, ReadOnly, Tooltip("Cached world position, based on origin's forward vector")]
        public Vector3 Originforward { get; set; } = Vector3.zero;
    }
}
