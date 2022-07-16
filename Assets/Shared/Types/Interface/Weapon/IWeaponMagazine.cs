public interface KD_IWeaponMagazine
{
    int magazineSize { get; }
    int amoution { get; }
    bool Busy { get; }
    TYPEWEAPON Type { get; }
    void consume(int amount = 1);
    void load(int amount = 1);
    void fill();
    bool hasAmmo();
}
