using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

public class RS_TankMovement : MonoBehaviour, IFireBullet
{

    [SerializeField] private GameObject bullet = null;
    [SerializeField] private Transform scope = null;
    [SerializeField] private Transform BetterScope = null;

    public ParticleSystem dustTrail;
    public ParticleSystem dustTrailBack;
    public Animator tankMoveAnime;

    private Text txtBulletCount = null;

    private Image imgBullet = null;

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

        scope = gameObject.transform.Find("Scope");
        BetterScope = gameObject.transform.Find("BetterScope");
        if (!scope)
            Debugger.Break();
        //}
    }

    private void InstaceBullet(int bullets = 1)
    {
        // if (photonView.IsMine)
        // {
        for (int i = 0; i < bullets; i++)
        {
            // start Shoot coroutine
        }
        //StartCoroutine(Shoot());
        Shoot();
        // }
    }

    List<GameObject> InOrbitBullets = new List<GameObject>();

    public void RegenerateBulletList(GameObject bullet)
    {
        InOrbitBullets.Remove(bullet);
    }

    // create a coroutine
    private void Shoot()
    {
        var bulletAmount = 10;
        var rotAngle = 360 / bulletAmount;
        for (int i = 0; i < bulletAmount; i++)
        {
            var bullet = Instantiate(this.bullet);
            InOrbitBullets.Add(bullet);

            var bulletController = bullet.GetComponent<BulletController>();
            bulletController.enabled = true;
            var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

            //convert vector to quaternion
            var newForward = Quaternion.Euler(0, rotAngle * i, 0) * BetterScope.transform.forward;
            newForward.Normalize();
            var newPosition = BetterScope.transform.position + newForward * 1.5f;

            bulletController.DesireedPosition = newPosition;

            bulletSettings.Init(gameObject, newPosition, newForward, weapon);
            bulletController.Init(bulletSettings);

            //yield return new WaitForSeconds(0.00f);
        }
    }

    float totalDelta = 0f;
    public float DeltaOffsetRate = 150f;
    public float PositionOffset = 2f;
    public float BulletsSpeed = 20f;
    private void UpdateBulletsPosition()
    {
        if (InOrbitBullets.Count == 0)
            return;

        var rotAngle = 360 / InOrbitBullets.Count;
        var idx = 0;
        foreach (var bullet in InOrbitBullets)
        {
            var pointY = (rotAngle * idx) + totalDelta;
            //convert vector to quaternion
            var newForward = Quaternion.Euler(0, pointY, 0) * BetterScope.transform.forward; //rotate forward vector
            newForward.Normalize();
            var newPosition = BetterScope.transform.position + newForward * PositionOffset; // ... + offset

            var bulletController = bullet.GetComponent<BulletController>();
            bulletController.DesireedPosition = newPosition;
            bulletController.BulletSpeed = BulletsSpeed;
            idx++;

            UnityEngine.Debug.DrawLine(newPosition, newPosition + Vector3.up * 3, Color.red);
        }
        totalDelta += Time.deltaTime * DeltaOffsetRate;
        if (totalDelta >= 360f)
            totalDelta = 0f;
    }

    void Update()
    {
        //if (photonView.IsMine)
        //{
        #region Movement
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
        #endregion
        //}
        #region UI

        //if (photonView.IsMine) 
        txtBulletCount.text = count.ToString();
        #endregion

        #region Shooting Time
        if (isShooting == true) timePerBullet += Time.deltaTime;
        #endregion

        #region Realoading
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
        #endregion

        #region Shoot Manual
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
        #endregion

        #region Shoot Automatic Weapons
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
        #endregion

        #region Shoot Realoading
        if (Input.GetKeyDown(KeyCode.R)) isRealoding = true;
        #endregion

        #region UI
        if (count == 0) txtBulletCount.text = "R";
        #endregion

        UpdateBulletsPosition();
    }

    private void Reload(int nBullets, float TimeToReaload)
    {
        // if (photonView.IsMine)
        // {
        if (timeToReaload > TimeToReaload)
        {
            isRealoding = false;
            count = nBullets;
            timeToReaload = 0;
        }
        // }
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
