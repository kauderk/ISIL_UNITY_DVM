using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform scope = null;

    private Text txtBulletCount = null;
    private Image imgBullet = null;

    private Rigidbody _rgb = null;

    private Vector3 direction = Vector3.zero;
    //private Vector3 rotation = Vector3.zero;

    private float moveX, moveZ, timePerBullet, timeToReaload = 0f;

    private int count, maxBullet = 0;

    private bool isRealoding, isShooting = false;

    [HideInInspector] public enum TYPEWEAPON { PISTOL, SHOTGUN, RIFLE };

    public TYPEWEAPON weapon = TYPEWEAPON.PISTOL;

    private void Awake()
    {
        _rgb = this.gameObject.GetComponent<Rigidbody>();

        txtBulletCount = GameObject.Find("txtBulletCount").GetComponent<Text>();
        imgBullet = GameObject.Find("BgBullet").GetComponent<Image>();

        maxBullet = 10;
        count = maxBullet;

        txtBulletCount.color = new Color(0, 0.751729f, 1, 1);
    }

    private void InstaceBullet(int bullets = 1)
    {
        for (int i = 0; i < bullets; i++)
        {
            var bulletInstance = GameObject.Instantiate(bullet);
            bulletInstance.GetComponent<BulletController>().Init(this.gameObject,scope);
        }
    }

    private void Update()
    {
        txtBulletCount.text = count.ToString();

        moveX = Input.GetAxisRaw("Horizontal");
        moveZ = Input.GetAxisRaw("Vertical");

        direction = new Vector3(moveX, 0, moveZ) * 10;
        transform.forward = direction.normalized;
        _rgb.velocity = direction;

        if(isShooting == true) timePerBullet += Time.deltaTime;

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
                        isShooting = false;
                        InstaceBullet();
                        count -= 1;
                    }
                    break;
                case TYPEWEAPON.SHOTGUN:
                    if (count > 0 && timePerBullet > 0.5f)
                    {
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
                        isShooting = false;
                        InstaceBullet();
                        count -= 1;
                        timePerBullet = 0f;
                    }
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) isRealoding = true;
        if(count == 0) txtBulletCount.text = "¡R!";

        #region multiplayers
        #endregion
    }


    private void Reload(int nBullets, float TimeToReaload)
    {
        if (timeToReaload > TimeToReaload)
        {
            isRealoding = false;
            count = nBullets;
            timeToReaload = 0;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Pistol"))
        {
            maxBullet = 10;
            weapon = TYPEWEAPON.PISTOL;
            count = maxBullet;
            txtBulletCount.color = new Color(0, 0.751729f, 1, 1);
        }
        else if (collision.gameObject.CompareTag("Rifle"))
        {
            maxBullet = 20;
            weapon = TYPEWEAPON.RIFLE;
            count = maxBullet;
            txtBulletCount.color = new Color(1, 0.2206753f, 0, 1);
        }
        else if (collision.gameObject.CompareTag("Shotgun"))
        {
            maxBullet = 6;
            weapon = TYPEWEAPON.SHOTGUN;
            count = maxBullet;
            txtBulletCount.color = new Color(0, 1, 0.0381248f, 1);
        }
    }

}
