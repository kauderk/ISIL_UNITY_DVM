using UnityEngine;

public interface KD_IWeaponShooter
{
    public int magazineSize { get; }
    public int amoution { get; }
    public int fireRate { get; }
    public float cadence { get; }
    public void Fire();
    public void Init(GameObject bulletPrefab, Transform scope);
    public bool CanFire(float deltaFireRate);
}
