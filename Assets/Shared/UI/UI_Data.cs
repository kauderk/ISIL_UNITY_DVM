using EventBusSystem;
using Weapon;

public interface IUIShootEvents : IGlobalSubscriber
{
    void OnShoot(PlayerStats player, SO_WeaponMagazine magazine);
}