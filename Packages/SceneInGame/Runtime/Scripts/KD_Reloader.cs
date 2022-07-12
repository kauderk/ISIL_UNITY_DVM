using UnityEngine;
using Photon.Pun;

public class KD_Reloader : MonoBehaviourPunCallbacks
{
    public SO_WeaponSettings Settings;

    KD_IWeapon Iweapon;
    float timeToReaload = 0f;

    private void Awake() => Iweapon = Settings;

    void Update()
    {
        if (InputReload() && !Iweapon.isReloading)
        {
            timeToReaload += Time.deltaTime;

            if (timeToReaload > Iweapon.reloadTime)
            {
                Iweapon.Reload();
                timeToReaload = 0;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //if (photonView.IsMine)
        //{
        if (collision.gameObject.TryGetComponent<KD_IMagazine>(out var magazine))
        {
            collision.gameObject.SetActive(false);
            // var settings = magazine.PickUp();
            //weapon = settings.weapon; // TODO:
            Iweapon.FillMagazine();
            // TODO: Fire a Gloabl Event to update the UI 
            //txtBulletCount.color = settings.color;
        }
        //}
    }

    bool InputReload() => Input.GetKeyDown(KeyCode.R);
}
