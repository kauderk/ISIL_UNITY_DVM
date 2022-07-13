using UnityEngine;

[CreateAssetMenu(fileName = "WeaponReloader", menuName = "Game Data/WeaponReloader")]
public class SO_WeaponReloader : ScriptableObject, KD_IWeaponReloader
{
    [field: SerializeField]
    public int reloadAmount { get; private set; }

    [field: SerializeField]
    public float reloadTime { get; private set; }

    [field: SerializeField]
    public SO_WeaponMagazine Magazine { get; private set; }


    public void FillMagazine() => Magazine.fill();

    public void Reload() // reloaing one by one could be a mechanic in it's own right
    {
        FillMagazine();
    }
}
