using UnityEngine;
using Photon.Pun;

public class KD_Shooter : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings settings;
    public KD_IWeapon weapon;
    public Transform scope;

    float deltaFireRate = 0f;

    private void Awake()
    {
        scope = gameObject.transform.Find("Scope"); //TODO:
        weapon.Init(settings.tankSettings.bulletPrefab, scope);
    }

    void Update()
    {
        if (InputIsShooting() && weapon.CanFire(deltaFireRate))
        {
            if (weapon.type == WeaponType.automatic)
                deltaFireRate = 0f; // reset timer

            weapon.Fire();
        }
        else if (InputIsShooting())
        {
            deltaFireRate += Time.deltaTime;
        }
    }

    bool InputIsShooting() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
}
