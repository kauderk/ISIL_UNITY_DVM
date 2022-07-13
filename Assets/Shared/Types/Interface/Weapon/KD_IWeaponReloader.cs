public interface KD_IWeaponReloader
{
    public int reloadAmount { get; }
    public float reloadTime { get; }
    public bool busy { get; }
    public void Reload();
    public void FillMagazine();
}
