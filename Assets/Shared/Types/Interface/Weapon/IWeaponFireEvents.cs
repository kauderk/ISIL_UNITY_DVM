namespace Weapon
{
    public interface IFireEvent
    {
        void OnFire();
        void OnStopFire();
        //void OnStartFire();
    }
    public interface IReloadEvent
    {
        void OnReload();
        //void OnEmpty();
        //void OnFull();
    }
    public interface IWeaponEvent : IFireEvent, IReloadEvent
    {
    }
}