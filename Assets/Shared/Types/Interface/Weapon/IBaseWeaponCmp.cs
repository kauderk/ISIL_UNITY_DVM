namespace Weapon
{
    public interface IBaseWeaponCmp
    {
        SO_WeaponMagazine Magazine { get; }
        SO_WeaponSFX SFX { get; }
    }
}