namespace Weapon
{
    public interface IMagazine
    {
        SO_WeaponSkin SkinSettings { get; }
        void PickUp();
    }
}
