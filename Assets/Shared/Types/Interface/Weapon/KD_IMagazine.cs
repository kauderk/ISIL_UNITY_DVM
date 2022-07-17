namespace Weapon
{
    public interface IMagazine
    {
        SO_WeaponMagazine settings { get; } // can't set private because the custom editors won't work otherwise
        SO_WeaponSkin skinSettings { get; } // can't set private because the custom editors won't work otherwise
        void PickUp();
    }
}
