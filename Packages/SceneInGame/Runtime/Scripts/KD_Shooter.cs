using UnityEngine;
using Photon.Pun;

public class KD_Shooter : MonoBehaviourPunCallbacks
{
    public SO_WeaponSettings Settings;
    public Transform scope;

    KD_IWeaponShooter Iweapon;
    float deltaFireRate = 0f;

    private void Awake()
    {
        scope = gameObject.transform.Find("Scope"); //TODO:
        Iweapon = Settings.shooter;
        Iweapon.Init(Settings.bulletSettings.Instance.bullet, scope);
    }

    void Update()
    {
        if (InputIsShooting() && Iweapon.CanFire(deltaFireRate))
        {
            if (Iweapon.type == WeaponType.automatic)
                deltaFireRate = 0f; // reset timer

            Iweapon.Fire();
        }
        else if (InputIsShooting())
        {
            deltaFireRate += Time.deltaTime;
        }
    }

    bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
}
