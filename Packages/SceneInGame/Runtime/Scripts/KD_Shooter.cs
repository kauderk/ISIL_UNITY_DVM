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
    public float reloadAmount { get; }
    public float reloadTime { get; }
    public WeaponType type { get; }
    public int fireRate { get; }
    public float cadence { get; set; }
    public void Fire();
    public void Reload();
}

public class WeaponClass : MonoBehaviour, IWeapon
{
    public float reloadAmount { get; } = 10f;
    public float reloadTime { get; } = 1f;
    public WeaponType type { get; }
    public float cadence { get; set; } = 0.1f;

    public int fireRate { get; } = 1;

    public void Reload()
    {
        //reloadAmount
        //reloadTime
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
    //private GameObject bullet;
    private Transform scope;


    float timeToReaload = 0f;
    float timePerBullet = 0f;
    int count, maxBullet = 0;
    bool isRealoding;
    bool isShooting = false;

    bool enoughTime(float time = 0.1f) => timePerBullet > time;

    bool hasAmmo() => count > 0;

    public IWeapon weapon;


    private void Awake()
    {
        settings.Init(gameObject);
        //TODO: weapon has to be an object
        //if (photonView.IsMine)
        //{
        //count = maxBullet = 10;
        scope = gameObject.transform.Find("Scope"); //TODO:
        //}
    }

    private void Fire(int bullets = 1)
    {
        isShooting = false;
        count -= 1;
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

    void Update()
    {
        // // timers
        // if (isShooting == true)
        //     timePerBullet += Time.deltaTime;

        // Reload();

        if (InputIsShooting() && hasAmmo() && enoughTime(weapon.cadence))
        {
            if (weapon.type == WeaponType.automatic)
                timePerBullet = 0f; // reset timer

            weapon.Fire();
        }

        // // realoading
        // if (Input.GetKeyDown(KeyCode.R))
        //     isRealoding = true;

        //UpdateUI();
    }

    private void Reload()
    {
        if (isRealoding == true)
        {
            timeToReaload += Time.deltaTime;
            weapon.Reload();

        }
    }

    private void Reload(int nBullets, float TimeToReaload)
    {
        if (photonView.IsMine)
        {
            if (timeToReaload > TimeToReaload)
            {
                isRealoding = false;
                count = nBullets;
                timeToReaload = 0;
            }
        }
    }

    // private void OnCollisionEnter(Collision collision)
    // {
    //     //if (photonView.IsMine)
    //     //{
    //     if (collision.gameObject.TryGetComponent<KD_IMagazine>(out var magazine))
    //     {
    //         collision.gameObject.SetActive(false);
    //         var settings = magazine.PickUp();
    //         weapon = settings.weapon;
    //         count = maxBullet = settings.magazineSize;
    //         // TODO: Fire a Gloabl Event to update the UI 
    //         txtBulletCount.color = settings.color;
    //     }
    //     //}
    // }
    bool InputIsShooting()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
            return isShooting = true;
        return false;
    }
}
