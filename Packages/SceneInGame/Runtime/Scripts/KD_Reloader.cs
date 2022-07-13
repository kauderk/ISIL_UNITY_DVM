using UnityEngine;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Weapon
{
    public class KD_Reloader : MonoBehaviourPunCallbacks
    {
        [DimmerAssign, Tooltip("Will render the Reloader Settings, if it isn't null.")]
        public SO_WeaponSettings WeaponSettings;
        [SerializeField, HideInInspector, Tooltip("Inherited from the WeaponSettings.")]
        public SO_WeaponReloader Settings;


        KD_IWeaponReloader Iweapon;
        float delta;
        bool busy;

        private void Awake() => Iweapon = Settings;

        void Update()
        {
            delta += Time.deltaTime;

            if (InputReload() && !busy)
            {
                busy = true;
                if (delta > Iweapon.reloadTime)
                {
                    Iweapon.FillMagazine();
                    delta = 0;
                    busy = false;
                }
            }
        }

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<KD_IMagazine>(out var magazine))
            {
                collision.gameObject.SetActive(false);
                //Debug.Break();
                //SO_RedirectSignalManager.Signal.PickUp(gameObject);
                SO_RedirectSignalManager.UseUpwardSignal<KD_ISignalListener>(gameObject);
                SO_RedirectSignalManager.UseLateralSignal<KD_ISignalListener>(gameObject);
                //var settings = magazine.PickUp();
                //Iweapon.FillMagazine();
                // TODO: Fire a Gloabl Event to update the UI 
                //txtBulletCount.color = settings.color;
            }
        }

        bool InputReload() => Input.GetKeyDown(KeyCode.R);
    }
#if UNITY_EDITOR
    [CanEditMultipleObjects]
    [CustomEditor(typeof(KD_Reloader))]
    public class KD_ReloaderEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (KD_Reloader)target;
            var wp = script.WeaponSettings;
            if (!wp)
            {
                EditorUtils.ErrorInspector(nameof(script.WeaponSettings));
                return;
            }

            // check if Shooter Settings is valid to show in the inspector
            script.Settings = EditorUtils.AssignField(wp.reloader, serializedObject.FindProperty(nameof(script.Settings)));
        }
    }
#endif
}