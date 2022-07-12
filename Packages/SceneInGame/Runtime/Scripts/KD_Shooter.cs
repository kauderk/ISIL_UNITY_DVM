using UnityEngine;
using Photon.Pun;

public class KD_Shooter : MonoBehaviourPunCallbacks
{
    public SO_WeaponShooter Settings;
    public Transform scope; // SOC_WeaponShooter ready to use if needed

    KD_IWeaponShooter IShooter;
    float deltaFireRate = 0f;

    private void Awake()
    {
        scope = gameObject.transform.Find("Scope"); //TODO:
        IShooter = Settings;
        //Iweapon.Init(Settings.bulletSettings.Instance.bullet, scope);
    }

    void Update()
    {
        if (InputIsShooting() && IShooter.CanFire(deltaFireRate))
        {
            if (IShooter.type == WeaponType.automatic)
                deltaFireRate = 0f; // reset timer

            IShooter.Fire();
        }
        else if (InputIsShooting())
        {
            deltaFireRate += Time.deltaTime;
        }
    }

    bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
}
