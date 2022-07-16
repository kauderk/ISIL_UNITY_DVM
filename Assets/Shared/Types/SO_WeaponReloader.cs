using UnityEngine;

namespace Weapon
{
    [CreateAssetMenu(fileName = "WeaponReloader", menuName = "Game Data/WeaponReloader")]
    public class SO_WeaponReloader : ScriptableObject, KD_IWeaponReloader, IBaseWeaponCmp
    {
        [field: SerializeField]
        public int Amount { get; private set; } = 1;

        [field: SerializeField]
        public float FullyReloadTime { get; private set; } = 5f;

        [field: SerializeField]
        public float SingleReloadTime { get; private set; } = 1.5f;
    }
}
