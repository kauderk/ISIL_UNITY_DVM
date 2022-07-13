using UnityEngine;

public class KD_WeaponComponent : MonoBehaviour, KD_IWeaponShooter
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
    public GameObject bullet { get; private set; }

    public bool CanFire(float deltaFireRate)
    {
        bool enoughTime = deltaFireRate > cadence;
        bool hasAmmo = amoution > 0;
        return hasAmmo && enoughTime;
    }

    public void Fire()
    {
        for (int i = 0; i < fireRate; i++)
        {
            var bullet = Instantiate(this.bullet);
            var controller = bullet.GetComponent<BulletController>();
            controller.enabled = false;

            var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

            bulletSettings.Init(bullet, gameObject, scope);
            controller.Init(bulletSettings);

            controller.enabled = true;
        }
    }


    public void FillMagazine() => amoution = magazineSize;

    public void Reload()
    {
        isReloading = true;
        if (amoution < magazineSize)
            amoution += reloadAmount;
        //reloadTime = 0;
    }

    public void Init(SOC_WeaponShooter EditorSettings)
    {
        throw new System.NotImplementedException();
    }

    public void Init(SOC_WeaponShooter EditorSettings, KD_IWeaponMagazine Magazine)
    {
        throw new System.NotImplementedException();
    }
}
