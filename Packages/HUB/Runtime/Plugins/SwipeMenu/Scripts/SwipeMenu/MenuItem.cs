using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SwipeMenu
{
    /// <summary>
    /// Attach to any menu item. 
    /// </summary>

    public class MenuItem : MonoBehaviour
    {
        public SO_SceneData SceneData;
        /// <summary>
        /// The behaviour to be invoked when the menu item is selected.
        /// </summary>
        [HideInInspector]
        public Button.ButtonClickedEvent OnClick;

        public void PopulateChildren()
        {
            OnComponent<SpriteRenderer>(sp => sp.sprite = SceneData.menuItemCover);
            OnComponent<TMP_Text>(tmp => tmp.text = SceneData.Name);
        }

        //TODO: MAKE STATIC FUNCTION
        private void OnComponent<T>(Action<T> cb)
        {
            var comp = GetComponentInChildren<T>();
            if (comp != null)
                cb(comp);
        }

        /// <summary>
        /// The behaviour to be invoked when another menu item is selected.
        /// </summary>
        public Button.ButtonClickedEvent OnOtherMenuClick;

    }
#if UNITY_EDITOR
    [CustomEditor(typeof(MenuItem)), CanEditMultipleObjects]
    public class MenuItemEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var script = (MenuItem)target;

            // add white space
            EditorGUILayout.Space();

            if (GUILayout.Button("Fill Children Properties", GUILayout.Height(40)))
            {
                //script.PopulateChildren();
                foreach (var obj in Selection.gameObjects)
                {
                    var other = obj.GetComponent<SwipeMenu.MenuItem>();
                    if (other && other != this)
                        other.PopulateChildren();
                }
            }
        }
    }
#endif
}
