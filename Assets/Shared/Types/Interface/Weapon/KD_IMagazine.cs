namespace Weapon
{
    public interface IMagazine
    {
        SO_AmmoSettings Settings { get; }
        void PickUp();
    }
}
