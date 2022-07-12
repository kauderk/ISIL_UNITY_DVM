using UnityEngine;
using Photon.Pun;

public class KD_Shooter : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings settings;
    //private GameObject bullet = null;
    //private Transform scope = null;


    //private Text txtBulletCount = null;

    //private Image imgBullet = null;

    //private TYPEWEAPON weapon = TYPEWEAPON.PISTOL;


    // private float timePerBullet, timeToReaload = 0f;
    // private int count, maxBullet = 0;
    // private bool isRealoding, isShooting = false;


    private void Awake()
    {
        settings.Init(gameObject);
        //if (photonView.IsMine)
        //{
        //txtBulletCount = GameObject.Find("txtBulletCount").GetComponent<Text>();
        //imgBullet = GameObject.Find("BgBullet").GetComponent<Image>();

        //count = maxBullet = 10;

        //txtBulletCount.color = new Color(0, 0.751729f, 1, 1);

        //scope = gameObject.transform.Find("Scope");
        //}
    }

    // private void Fire(int bullets = 1)
    // {
    //     if (photonView.IsMine)
    //     {
    //         for (int i = 0; i < bullets; i++)
    //         {
    //             var bullet = Instantiate(this.bullet);
    //             var controller = bullet.GetComponent<BulletController>();
    //             controller.enabled = false;

    //             var settings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

    //             settings.Init(gameObject, scope, weapon);
    //             controller.Init(settings);

    //             controller.enabled = true;
    //         }
    //     }
    // }

    void Update()
    {
        // // timers
        // if (isShooting == true)
        //     timePerBullet += Time.deltaTime;

        // Reload();
        // ShootManual();
        // ShootAutomatic();

        // // realoading
        // if (Input.GetKeyDown(KeyCode.R))
        //     isRealoding = true;

        //UpdateUI();
    }

    // private void UpdateUI()
    // {
    //     if (photonView.IsMine) txtBulletCount.text = count.ToString();
    //     if (count == 0 && photonView.IsMine) txtBulletCount.text = "R";
    // }

    // private void ShootAutomatic()
    // {
    //     if (Input.GetMouseButton(0))
    //     {
    //         isShooting = true;

    //         switch (weapon)
    //         {
    //             case TYPEWEAPON.RIFLE:
    //                 if (count > 0 & timePerBullet > 0.1f)
    //                 {
    //                     isShooting = false;
    //                     Fire();
    //                     count -= 1;
    //                     timePerBullet = 0f;
    //                 }
    //                 break;
    //         }
    //     }
    // }

    // private void ShootManual()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         isShooting = true;
    //         switch (weapon)
    //         {
    //             case TYPEWEAPON.PISTOL:

    //                 if (count > 0 && timePerBullet > 0.17f)
    //                 {
    //                     isShooting = false;
    //                     Fire();
    //                     count -= 1;
    //                 }
    //                 break;
    //             case TYPEWEAPON.SHOTGUN:
    //                 if (count > 0 && timePerBullet > 0.5f)
    //                 {
    //                     isShooting = false;
    //                     Fire(3);
    //                     count -= 1;
    //                     timePerBullet = 0f;
    //                 }
    //                 break;
    //         }
    //     }
    // }

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
}
