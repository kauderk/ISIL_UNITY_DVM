using UnityEngine;
using EventBusSystem;
using UnityEngine.UI;
using Weapon;
using TMPro;
using System;

public class UIController : MonoBehaviour, IUIShootEvents, IMultiplayerSubscriber
{
    //private void Awake() => ;
    private void OnDisable() => EventBus.Unsubscribe(this);

    public TMP_Text AmmoText;
    public TMP_Text HelathText;
    public TMP_Text UserOrderText;

    private void Awake()
    {
        var test = AmmoText.text +
        HelathText.text +
        UserOrderText.text;
        Debug.Log(test + "UI IS REFERENCED!");
        EventBus.Subscribe(this);
    }

    public void OnMagazineChange(PlayerStats player, SO_WeaponMagazine magazine)
    {
        // react to ammo change
        AmmoText.text = magazine.Amoution.ToString();

        if (magazine.Amoution <= 0) AmmoText.text = "R";
    }

    public void OnHealthChange(PlayerStats player)
    {
        try
        {
            HelathText.text = player.Health.ToString();
        }
        catch (System.Exception)
        {
            Debug.LogError("Health is not set");
            //System.Diagnostics.Debugger.Break();
            //throw;
        }
    }

    public void OnPlayerInstaceCreated(PlayerStats player)
    {
        try
        {
            // 1 / 4
            UserOrderText.text = player.Order.ToString() + "/" + Enum.GetNames(typeof(Players)).Length.ToString();
        }
        catch (System.Exception)
        {
            Debug.LogError("Order is not set");
            //System.Diagnostics.Debugger.Break();
            //throw;
        }
    }
}
