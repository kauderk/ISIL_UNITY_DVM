using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;

public class RS_TankMovement : MonoBehaviourPunCallbacks
{

    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform scope = null;

    public ParticleSystem dustTrail;
    public ParticleSystem dustTrailBack;
    public Animator tankMoveAnime;

    private Text txtBulletCount = null;

    private Image imgBullet = null;    

    [HideInInspector] public enum TYPEWEAPON { PISTOL, SHOTGUN, RIFLE };
    public TYPEWEAPON weapon = TYPEWEAPON.PISTOL;

    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    private float timePerBullet, timeToReaload = 0f;

    private int count, maxBullet = 0;

    private bool isRealoding, isShooting = false;


    private void Awake()
    {
        //if (photonView.IsMine)
        //{
            txtBulletCount = GameObject.Find("txtBulletCount").GetComponent<Text>();
            imgBullet = GameObject.Find("BgBullet").GetComponent<Image>();

            maxBullet = 10;
            count = maxBullet;

            txtBulletCount.color = new Color(0, 0.751729f, 1, 1);
        //}
    }

    private void InstaceBullet(int bullets = 1)
    {
        if (photonView.IsMine)
        {
            for (int i = 0; i < bullets; i++)
            {
                GameObject bulletInstance = bullet;//Instantiate(bullet);
                Instantiate(bulletInstance);
                //bulletInstance.GetComponent<BulletController>().Init(this.gameObject, scope);
            }
        }
    }
    
    void Update()
    {
        //if (photonView.IsMine)
        //{
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                dustTrail.Play();
                tankMoveAnime.SetBool("IsMoving", true);
            }

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
            {
                dustTrail.Stop();
                tankMoveAnime.SetBool("IsMoving", false);
            }

            if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                dustTrailBack.Play();
                tankMoveAnime.SetBool("IsMoving", true);
            }

            if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
            {
                dustTrailBack.Stop();
                tankMoveAnime.SetBool("IsMoving", false);
            }
        //}

        if (photonView.IsMine) txtBulletCount.text = count.ToString();

        if (isShooting == true) timePerBullet += Time.deltaTime;

        if (isRealoding == true)
        {
            timeToReaload += Time.deltaTime;
            switch (weapon)
            {
                case TYPEWEAPON.PISTOL:
                    Reload(10, 0.5f);
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
        if (count == 0 && photonView.IsMine) txtBulletCount.text = "R";
        
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
