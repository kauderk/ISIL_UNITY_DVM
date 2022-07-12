using UnityEngine;
using Photon.Pun;

public interface IWeapon
{
    public float cadence { get; set; }
    public void Fire();
}

public class WeaponClass : MonoBehaviour, IWeapon
{
    public float cadence { get; set; } = 0.1f;

    public void Fire()
    {
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


    // private bool isRealoding, 
    // private float timePerBullet, timeToReaload = 0f;
    float timePerBullet = 0f;
    int count, maxBullet = 0;
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
        if (InputIsShooting() && hasAmmo())
        {
            // ShootManual
            if (enoughTime(weapon.cadence))
                weapon.Fire();
            // ShootAutomatic
            if (enoughTime())
                FireAndReset();
        }

        // // realoading
        // if (Input.GetKeyDown(KeyCode.R))
        //     isRealoding = true;

        //UpdateUI();
    }

    public void ChangeWeapon()
    {

    }



    private void FireAndReset(int bullets = 1)
    {
        Fire(bullets);
        timePerBullet = 0f; // reset timer
    }



    // private void Reload()
    // {
    //     if (isRealoding == true)
    //     {
    //         timeToReaload += Time.deltaTime;
    //         switch (weapon)
    //         {
    //             case TYPEWEAPON.PISTOL:
    //                 Reload(10, 0.5f);
    //                 break;
    //             case TYPEWEAPON.SHOTGUN:
    //                 Reload(6, 0.5f);
    //                 break;
    //             case TYPEWEAPON.RIFLE:
    //                 Reload(20, 1f);
    //                 break;
    //         }
    //     }
    // }

    // private void Reload(int nBullets, float TimeToReaload)
    // {
    //     if (photonView.IsMine)
    //     {
    //         if (timeToReaload > TimeToReaload)
    //         {
    //             isRealoding = false;
    //             count = nBullets;
    //             timeToReaload = 0;
    //         }
    //     }
    // }

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
