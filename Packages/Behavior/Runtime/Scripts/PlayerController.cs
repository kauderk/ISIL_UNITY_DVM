using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject bullet = null;

    private Text txtBulletCount = null;
    private Image imgBullet = null;

    private Rigidbody _rgb = null;

    private Vector3 direction = Vector3.zero;
    //private Vector3 rotation = Vector3.zero;

    private float moveX, moveZ, timePerBullet = 0f;

    private int count, maxBullet = 0;

    [SerializeField] private LayerMask mask;

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
            GameObject.Instantiate(bullet);
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

        if (Input.GetMouseButtonDown(0) & timePerBullet > 0.17f)
        {
            timePerBullet += Time.deltaTime;

            switch (weapon)
            {
                case TYPEWEAPON.PISTOL:

                    if (count > 0)
                    {
                        InstaceBullet();
                        count -= 1;
                    }
                    break;
                case TYPEWEAPON.SHOTGUN:
                    if (count > 0 & timePerBullet > 0.21f)
                    {
                        InstaceBullet(3);
                        count -= 1;
                        timePerBullet = 0f;
                    }
                    break;
            }
        }

        if (Input.GetMouseButton(0))
        {
            timePerBullet += Time.deltaTime;

            switch (weapon)
            {
                case TYPEWEAPON.RIFLE:
                    if (count > 0 & timePerBullet > 0.1f)
                    {
                        InstaceBullet();
                        count -= 1;
                        timePerBullet = 0f;
                    }
                    break;
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            switch (weapon)
            {
                case TYPEWEAPON.PISTOL:
                    count = 10;
                    break;
                case TYPEWEAPON.SHOTGUN:
                    count = 6;
                    break;
                case TYPEWEAPON.RIFLE:
                    count = 20;
                    break;
            }
        } 

        if(count == 0)
        {
            txtBulletCount.text = "R!";
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
