using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponSkin", menuName = "Game Data/WeaponSkin")]
    public class SO_WeaponSkin : ScriptableObject
    {
        [field: SerializeField]
        public Color Color { get; private set; } = Color.gray;
    }
}
