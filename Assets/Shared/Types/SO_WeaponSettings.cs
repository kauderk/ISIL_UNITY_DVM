using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KD_WeaponSettings
{
    [field: SerializeField]
    public Animator animator { get; private set; }
    [field: SerializeField]
    public ParticleSystem dustTrail { get; private set; }
    [field: SerializeField]
    public ParticleSystem dustTrailBack { get; private set; }
}

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game Data/WeaponSettings")]
public class SO_WeaponSettings : ScriptableObject, KD_IWeapon
{
    [field: SerializeField]
    public int magazineSize { get; private set; }

    [field: SerializeField]
    public int amoution { get; private set; }

    [field: SerializeField]
    public int reloadAmount { get; private set; }

    [field: SerializeField]
    public float reloadTime { get; private set; }

    [field: SerializeField]
    public WeaponType type { get; private set; }

    [field: SerializeField]
    public int fireRate { get; private set; }

    [field: SerializeField]
    public float cadence { get; private set; }

    [field: SerializeField]
    public bool isReloading { get; private set; }

    [field: SerializeField]
    public TYPEWEAPON weapon { get; private set; }

    [field: SerializeField]
    public Transform scope { get; private set; }

    [field: SerializeField]
    public GameObject bulletPrefab { get; private set; }

    public bool CanFire(float deltaFireRate)
    {
        throw new System.NotImplementedException();
    }

    public void FillMagazine()
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

    public void Reload()
    {
        throw new System.NotImplementedException();
    }
}
