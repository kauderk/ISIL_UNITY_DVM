using UnityEngine;
using Photon.Pun;

namespace Weapon
{
    public abstract class KD_MonoWeapon : MonoBehaviourPunCallbacks
    {
        [DimmerAssign, Tooltip("Will render Settings, if this isn't null.")]
        public SO_WeaponSettings WeaponSettings;
    }
}