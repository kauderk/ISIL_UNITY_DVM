namespace Weapon
{
    public interface KD_IWeaponReloader
    {
        int reloadAmount { get; }
        float fullyReloadTime { get; }
        float SingleReloadTime { get; }
    }
}