using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Weapon
{
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
            script.Settings = EditorUtils.AssignField(wp.Shooter, serializedObject.FindProperty(nameof(script.Settings)));

            if (!wp.Ammo) // check if the bullet settings is null
                EditorUtils.ErrorInspector(nameof(wp.Ammo));
        }
    }
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
            script.Settings = EditorUtils.AssignField(wp.Reloader, serializedObject.FindProperty(nameof(script.Settings)));
        }
    }
#endif
}
