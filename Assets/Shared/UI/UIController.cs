using UnityEngine;
using EventBusSystem;
using UnityEngine.UI;
using Weapon;
using TMPro;

public class UIController : MonoBehaviour, IUIShootEvents
{
    private void OnEnable() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);

    public TMP_Text AmmoText;

    public void OnShoot(PlayerStats player, SO_WeaponMagazine magazine)
    {
        // react to ammo change
        AmmoText.text = magazine.Amoution.ToString();
    }
}
