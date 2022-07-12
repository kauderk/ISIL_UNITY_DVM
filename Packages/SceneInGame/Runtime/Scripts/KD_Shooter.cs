using UnityEngine;
using Photon.Pun;

public enum WeaponType
{
    automatic,
    semiAutomatic,
    singleShot,
}
public interface IWeapon
{
    public int magazineSize { get; set; }
    public int amoution { get; set; }
    public int reloadAmount { get; }
    public float reloadTime { get; }
    public WeaponType type { get; }
    public int fireRate { get; }
    public float cadence { get; set; }
    public bool isReloading { get; set; }
    public void Fire();
    public void Reload();
    public void FillMagazine();
}

public class WeaponClass : MonoBehaviour, IWeapon
{
    public int magazineSize { get; set; } = 10;
    public int amoution { get; set; } = 10;
    public int reloadAmount { get; } = 10;
    public float reloadTime { get; set; } = 1f;
    public WeaponType type { get; }
    public int fireRate { get; } = 1;
    public float cadence { get; set; } = 0.1f;
    public bool isReloading { get; set; } = false;

    public void Init()
    {
        FillMagazine();
    }

    public void FillMagazine() => amoution = magazineSize;

    public void Reload()
    {
        //reloadAmount
        //reloadTime
        isReloading = false;
        amoution += reloadAmount;
        reloadTime = 0;
        throw new System.NotImplementedException();
    }
    public void Fire()
    {
        // fireRate
        throw new System.NotImplementedException();
    }

    public void FireAndReset(float time)
    {
        throw new System.NotImplementedException();
    }
}

public class KD_Shooter : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings settings;
    private Transform scope;

    float timePerBullet = 0f;
    bool canShoot = false;

    bool enoughTime(float time = 0.1f) => timePerBullet > time;

    bool hasAmmo() => weapon.amoution > 0;

    public IWeapon weapon;


    private void Awake()
    {
        settings.Init(gameObject);
        //if (photonView.IsMine)
        //{
        scope = gameObject.transform.Find("Scope"); //TODO:
        //}
    }

    void Update()
    {
        if (InputIsShooting() && hasAmmo() && enoughTime(weapon.cadence))
        {
            if (weapon.type == WeaponType.automatic)
                timePerBullet = 0f; // reset timer

            weapon.Fire();
            canShoot = false;
        }
        else if (InputIsShooting())
        {
            timePerBullet += Time.deltaTime;
        }
    }

    private void Fire(int bullets = 1)
    {
        //if (photonView.IsMine)
        //{
        for (int i = 0; i < bullets; i++)
        {
            var bullet = Instantiate(settings.bullet);
            var controller = bullet.GetComponent<BulletController>();
            controller.enabled = false;

            var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

            bulletSettings.Init(gameObject, scope, settings.weapon);
            controller.Init(bulletSettings);

            controller.enabled = true;
        }
        //}
    }

    bool InputIsShooting()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            return canShoot = true;
        return false;
    }
}
