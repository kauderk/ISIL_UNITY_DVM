using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject bullet = null;

    private Text txtBulletCount = null;
    private Image imgBullet = null;

    private Rigidbody _rgb = null;

    private Vector3 direction = Vector3.zero;
    //private Vector3 rotation = Vector3.zero;

    private float moveX, moveZ, timePerBullet, timeToReaload = 0f;

    private int count, maxBullet = 0;

    private bool isRealoding, isShooting = false;

    public TYPEWEAPON weapon = TYPEWEAPON.PISTOL;

    private void Awake()
    {
        _rgb = this.gameObject.GetComponent<Rigidbody>();

        if (photonView.IsMine)
        {
            txtBulletCount = GameObject.Find("txtBulletCount").GetComponent<Text>();
            imgBullet = GameObject.Find("BgBullet").GetComponent<Image>();

            maxBullet = 10;
            count = maxBullet;

            txtBulletCount.color = new Color(0, 0.751729f, 1, 1);
        }
    }

    private void InstaceBullet(int bullets = 1)
    {
        for (int i = 0; i < bullets; i++)
        {
            var b = Resources.Load<GameObject>(bullet.name);
            // var b = Instantiate(bullet);
            var bc = b.GetComponent<BulletController>();
            bc.enabled = true;
            //b.GetComponent<BulletController>().Init(gameObject, scope);
            //Instantiate(b);
        }
    }

    private void Update()
    {
        if (photonView.IsMine) txtBulletCount.text = count.ToString();

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        direction = new Vector3(moveX, 0, moveZ) * 10;
        this.transform.forward = direction.normalized;
        _rgb.velocity = direction;

        if (isShooting == true) timePerBullet += Time.deltaTime;

        if (isRealoding == true)
        {
            timeToReaload += Time.deltaTime;
            switch (weapon)
            {
                case TYPEWEAPON.PISTOL:
                    //Reload(10, 1f);
                    if (timeToReaload >= 0.5f)
                    {
                        isRealoding = false;
                        count = 10;
                        timeToReaload = 0f;
                    }
                    break;
                case TYPEWEAPON.SHOTGUN:
                    Reload(6, 0.5f);
                    break;
                case TYPEWEAPON.RIFLE:
                    Reload(20, 1f);
                    break;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            switch (weapon)
            {
                case TYPEWEAPON.PISTOL:

                    if (count > 0 && timePerBullet > 0.17f)
                    {
                        print("Shooting");
                        isShooting = false;
                        InstaceBullet();
                        count -= 1;
                    }
                    break;
                case TYPEWEAPON.SHOTGUN:
                    if (count > 0 && timePerBullet > 0.5f)
                    {
                        print("Shooting");
                        isShooting = false;
                        InstaceBullet(3);
                        count -= 1;
                        timePerBullet = 0f;
                    }
                    break;
            }
        }

        if (Input.GetMouseButton(0))
        {
            isShooting = true;

            switch (weapon)
            {
                case TYPEWEAPON.RIFLE:
                    if (count > 0 & timePerBullet > 0.1f)
                    {
                        print("Shooting");
                        isShooting = false;
                        InstaceBullet();
                        count -= 1;
                        timePerBullet = 0f;
                    }
                    break;
            }
        }
        if (photonView.IsMine)
        {
            if (Input.GetKeyDown(KeyCode.R)) isRealoding = true;
            if (count == 0) txtBulletCount.text = "R";
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

    private void OnCollisionEnter(Collision collision)
    {
        //if (photonView.IsMine)
        //{
        if (collision.gameObject.CompareTag("Pistol"))
        {
            maxBullet = 10;
            weapon = TYPEWEAPON.PISTOL;
            count = maxBullet;
            txtBulletCount.color = new Color(0, 0.751729f, 1, 1);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Rifle"))
        {
            maxBullet = 20;
            weapon = TYPEWEAPON.RIFLE;
            count = maxBullet;
            txtBulletCount.color = new Color(1, 0.2206753f, 0, 1);
            collision.gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("Shotgun"))
        {
            maxBullet = 6;
            weapon = TYPEWEAPON.SHOTGUN;
            count = maxBullet;
            txtBulletCount.color = new Color(0, 1, 0.0381248f, 1);
            collision.gameObject.SetActive(false);
        }
        //}
    }

}
