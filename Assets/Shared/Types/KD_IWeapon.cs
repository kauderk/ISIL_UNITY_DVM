using UnityEngine;

public interface KD_IWeapon
{
    public int magazineSize { get; }
    public int amoution { get; }
    public int reloadAmount { get; }
    public float reloadTime { get; }
    public WeaponType type { get; }
    public int fireRate { get; }
    public float cadence { get; }
    public bool isReloading { get; }
    public TYPEWEAPON weapon { get; }
    public Transform scope { get; }
    public GameObject bulletPrefab { get; }
    public void Fire();
    public void Reload();
    public void FillMagazine();
    public void Init(GameObject bulletPrefab, Transform scope);
    public bool CanFire(float deltaFireRate);
}
