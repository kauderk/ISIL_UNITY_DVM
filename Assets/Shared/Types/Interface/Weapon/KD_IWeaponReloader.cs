public interface KD_IWeaponReloader
{
    public int reloadAmount { get; }
    public float reloadTime { get; }
    public bool isReloading { get; }
    public void Reload();
    public void FillMagazine();
}