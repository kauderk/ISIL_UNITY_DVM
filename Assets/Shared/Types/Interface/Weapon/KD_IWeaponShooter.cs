using UnityEngine;

public interface KD_IWeaponShooter
{
    public WeaponType type { get; } // The actual Weapon should have this property.
    public int fireRate { get; }
    public float cadence { get; }
    public GameObject bullet { get; }
    public void Fire();
    public void Init(SOC_WeaponShooter EditorSettings);
    public bool CanFire(float deltaFireRate);
}
[System.Serializable]
public class SOC_WeaponShooter
{
    // FIXME:
    [field: SerializeField]
    public Transform scope { get; private set; }

    [field: SerializeField]
    public GameObject caster { get; private set; }
}