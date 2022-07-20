using UnityEngine;
using EventBusSystem;
using UnityEngine.UI;
using Weapon;

public class UIController : MonoBehaviour, IUIShootEvents
{
    private void OnEnable() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);

    public Text ammoText;

    public void OnShoot(PlayerStats player, SO_WeaponMagazine magazine)
    {
        // react to ammo change
    }
}
