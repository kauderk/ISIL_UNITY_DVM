public interface KD_IWeaponMagazine
{
    public int magazineSize { get; }
    public int amoution { get; }
    public void consume(int amount = 1);
    public void load(int amount = 1);
    public void fill();
}
