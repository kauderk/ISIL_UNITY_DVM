namespace Weapon
{
    public interface IMagazine
    {
        SO_WeaponMagazine Settings { get; }
        SO_WeaponSkin SkinSettings { get; }
        void PickUp();
    }
}
