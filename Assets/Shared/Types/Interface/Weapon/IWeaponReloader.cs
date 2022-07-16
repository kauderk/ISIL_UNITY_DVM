namespace Weapon
{
    public interface KD_IWeaponReloader
    {
        int reloadAmount { get; }
        float reloadTime { get; }
        void Init(SO_WeaponMagazine Magazine);
        void Reload();
        void FillMagazine();
    }
}