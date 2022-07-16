namespace Weapon
{
    public interface KD_IWeaponReloader
    {
        int Amount { get; }
        float FullyReloadTime { get; }
        float SingleReloadTime { get; }
    }
}