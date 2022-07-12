using UnityEngine;

public interface KD_IWeapon : KD_IWeaponShooter, KD_IWeaponReloader
{
    public WeaponType type { get; }
    //public TYPEWEAPON weapon { get; }
    //public Transform scope { get; }
    //public GameObject bulletPrefab { get; }
}
