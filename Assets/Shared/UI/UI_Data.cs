using EventBusSystem;
using Weapon;

public interface IUIShootEvents : IGlobalSubscriber
{
    void OnMagazineChange(PlayerStats player, SO_WeaponMagazine magazine);
    void OnHealthChange(PlayerStats player);
}