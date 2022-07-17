using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSkin", menuName = "Game Data/WeaponSkin")]
    public class SO_WeaponSkin : ScriptableObject
    {
        [field: SerializeField]
        public SO_State<Color> Color { get; private set; } = new SO_State<Color>() { Editor = UnityEngine.Color.black };

        private void OnEnable()
        {
            if (Color.ResetOnLoad) // hardcoded for now FIXME:
                Color.Runtime = Color.Editor;
        }
    }
}
