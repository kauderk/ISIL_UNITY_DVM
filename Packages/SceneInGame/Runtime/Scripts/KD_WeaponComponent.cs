using UnityEngine;

public class KD_WeaponComponent : MonoBehaviour, KD_IWeapon
{
    public int magazineSize { get; } = 10;
    public int amoution { get; private set; } = 10;
    public int reloadAmount { get; } = 10;
    public float reloadTime { get; private set; } = 1f;
    public WeaponType type { get; }
    public int fireRate { get; } = 1;
    public float cadence { get; } = 0.1f;
    public bool isReloading { get; private set; } = false;
    public TYPEWEAPON weapon { get; }
    public Transform scope { get; private set; }
    public GameObject bulletPrefab { get; private set; }

    public void Init(GameObject bulletPrefab, Transform scope)
    {
        this.scope = scope;
        FillMagazine();
    }

    public void FillMagazine() => amoution = magazineSize;

    public void Reload()
    {
        isReloading = true;
        if (amoution < magazineSize)
            amoution += reloadAmount;
        //reloadTime = 0;
    }

    public void Fire()
    {
        for (int i = 0; i < fireRate; i++)
        {
            var bullet = Instantiate(bulletPrefab);
            var controller = bullet.GetComponent<BulletController>();
            controller.enabled = false;

            var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

            bulletSettings.Init(bullet, gameObject, scope, weapon);
            controller.Init(bulletSettings);

            controller.enabled = true;
        }
    }

    public bool CanFire(float deltaFireRate)
    {
        bool enoughTime = deltaFireRate > cadence;
        bool hasAmmo = amoution > 0;
        return hasAmmo && enoughTime;
    }
}
