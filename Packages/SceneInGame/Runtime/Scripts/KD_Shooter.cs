using UnityEngine;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Weapon
{
    public class KD_Shooter : MonoBehaviourPunCallbacks
    {
        [DimmerAssign, Tooltip("Will render the Shooter Settings, if it isn't null.")]
        public SO_WeaponSettings WeaponSettings;
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponShooter Settings;
        public SOC_WeaponShooter EditorSettings;


        IWeaponShooter IShooter;
        float deltaFireRate = 0f;

        void Awake()
        {
            IShooter = WeaponSettings.shooter; //SO_WeaponShooter
            IShooter.Init(WeaponSettings.Magazine, WeaponSettings.bulletSettings, EditorSettings);
        }

        void Update()
        {
            if (InputIsShooting() && IShooter.CanFire(deltaFireRate))
            {
                if (IShooter.Type == WeaponType.automatic)
                    deltaFireRate = 0f; // reset timer

                IShooter.Fire();
            }
            else if (InputIsShooting())
            {
                deltaFireRate += Time.deltaTime;
            }
        }

        bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    }
#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(KD_Shooter))]
    public class KD_ShooterEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (KD_Shooter)target;
            var wp = script.WeaponSettings;
            if (!wp)
            {
                EditorUtils.ErrorInspector(nameof(script.WeaponSettings));
                return;
            }

            // check if Shooter Settings is valid to show in the inspector
            script.Settings = EditorUtils.AssignField(wp.shooter, serializedObject.FindProperty(nameof(script.Settings)));

            if (!wp.bulletSettings) // check if the bullet settings is null
                EditorUtils.ErrorInspector(nameof(wp.bulletSettings));
        }
    }
#endif
}