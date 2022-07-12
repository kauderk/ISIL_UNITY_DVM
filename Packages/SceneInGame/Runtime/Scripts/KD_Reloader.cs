using UnityEngine;
using Photon.Pun;

public class KD_Reloader : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings settings;
    public KD_IWeapon weapon;

    float timeToReaload = 0f;

    void Update()
    {
        if (InputReload() && !weapon.isReloading)
        {
            timeToReaload += Time.deltaTime;

            if (timeToReaload > weapon.reloadTime)
            {
                weapon.Reload();
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
            weapon.FillMagazine();
            // TODO: Fire a Gloabl Event to update the UI 
            //txtBulletCount.color = settings.color;
        }
        //}
    }

    bool InputReload() => Input.GetKeyDown(KeyCode.R);
}
