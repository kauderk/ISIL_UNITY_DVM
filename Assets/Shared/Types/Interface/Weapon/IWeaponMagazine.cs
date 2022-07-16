namespace Weapon
{
    public interface IWeaponMagazine
    {
        int MagazineSize { get; }
        int Amoution { get; }
        bool Busy { get; }
        TYPEWEAPON Type { get; }
        void Consume(int amount = 1);
        void Load(int amount = 1);
        void Fill();
        bool HasAmmo();
        void Clear();
    }
}
