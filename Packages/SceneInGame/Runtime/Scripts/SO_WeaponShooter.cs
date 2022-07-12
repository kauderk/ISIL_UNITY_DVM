using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponShooter", menuName = "Game Data/WeaponShooter")]
public class SO_WeaponShooter : ScriptableObject, KD_IWeaponShooter
{
    [field: SerializeField]
    public WeaponType type { get; private set; }

    [field: SerializeField]
    public int magazineSize { get; private set; }

    [field: SerializeField]
    public int amoution { get; private set; }

    [field: SerializeField]
    public int fireRate { get; private set; }

    [field: SerializeField]
    public float cadence { get; private set; }

    [field: SerializeField, Tooltip("The actual Bullet Model.")]
    public GameObject bullet { get; set; }

    private SOC_WeaponShooter EditorSettings;


    public bool CanFire(float deltaFireRate)
    {
        bool enoughTime = deltaFireRate > cadence;
        bool hasAmmo = amoution > 0;
        return hasAmmo && enoughTime;
    }

    public void Fire()
    {
        for (int i = 0; i < fireRate; i++)
        {
            var bullet = Instantiate(this.bullet);
            var controller = bullet.GetComponent<BulletController>();
            controller.enabled = false;

            var bulletSettings = ScriptableObject.Instantiate(Resources.Load("Pistol")) as SO_BulletSettings;

            bulletSettings.Init(bullet, EditorSettings.caster, EditorSettings.scope);
            controller.Init(bulletSettings);

            controller.enabled = true;
        }
    }

    public void Init(SOC_WeaponShooter EditorSettings)
    {
        this.EditorSettings = EditorSettings;
    }
}
