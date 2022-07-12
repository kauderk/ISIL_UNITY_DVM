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
    public int magazineSize => throw new System.NotImplementedException();

    public int amoution => throw new System.NotImplementedException();

    public int reloadAmount => throw new System.NotImplementedException();

    public float reloadTime => throw new System.NotImplementedException();

    public WeaponType type => throw new System.NotImplementedException();

    public int fireRate => throw new System.NotImplementedException();

    public float cadence => throw new System.NotImplementedException();

    public bool isReloading => throw new System.NotImplementedException();

    public TYPEWEAPON weapon => throw new System.NotImplementedException();

    public Transform scope => throw new System.NotImplementedException();

    public GameObject bulletPrefab => throw new System.NotImplementedException();

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
