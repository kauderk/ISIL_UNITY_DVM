using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KD_SO_WeaponShooter
{
    [field: SerializeField]
    public Transform scope { get; private set; }
}

[CreateAssetMenu(fileName = "WeaponShooter", menuName = "Game Data/WeaponShooter")]
public class SO_WeaponShooter : ScriptableObject, KD_IWeaponShooter
{
    [field: SerializeField]
    public WeaponType type { get; private set; }

    [field: SerializeField]
    public int magazineSize { get; private set; }

    [field: SerializeField]
    public int amoution { get; private set; }

    [field: SerializeField]
    public int fireRate { get; private set; }

    [field: SerializeField]
    public float cadence { get; private set; }

    public bool CanFire(float deltaFireRate)
    {
        throw new System.NotImplementedException();
    }

    public void Fire()
    {
        throw new System.NotImplementedException();
    }

    public void Init(GameObject bulletPrefab, Transform scope)
    {
        throw new System.NotImplementedException();
    }
}
