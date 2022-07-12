using UnityEngine;

[CreateAssetMenu(fileName = "WeaponReloader", menuName = "Game Data/WeaponReloader")]
public class SO_WeaponReloader : ScriptableObject, KD_IWeaponReloader
{
    [field: SerializeField]
    public int reloadAmount { get; private set; }

    [field: SerializeField]
    public float reloadTime { get; private set; }

    [field: SerializeField]
    public bool isReloading { get; private set; }

    public void FillMagazine()
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        throw new System.NotImplementedException();
    }
}
