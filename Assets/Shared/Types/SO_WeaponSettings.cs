using UnityEngine;

[System.Serializable]
public class KD_WeaponSettings
{
    [field: SerializeField]
    public Transform scope { get; private set; }
}

[CreateAssetMenu(fileName = "WeaponSettings", menuName = "Game Data/WeaponSettings")]
public class SO_WeaponSettings : ScriptableObject
{
    public SO_WeaponShooter shooter;
    public SO_WeaponReloader reloader;
    public SO_BulletSettings bulletSettings;
}
