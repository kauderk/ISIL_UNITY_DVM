using UnityEngine;
using EventBusSystem;
using UnityEngine.UI;
using Weapon;
using TMPro;
using System;

public class UIController : MonoBehaviour, IUIShootEvents, IMultiplayerSubscriber
{
    private void Awake() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);

    public TMP_Text AmmoText;
    public TMP_Text HelathText;
    public TMP_Text UserOrderText;
    string dummy = "";

    private void OnEnable()
    {
        // var test = AmmoText.text +
        // HelathText.text +
        // UserOrderText.text;
        // Debug.Log(test + "UI IS REFERENCED!");
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
        catch (Exception e)
        {
            if (dummy?.Length == 0)
                dummy = e.Message;
            //Debug.LogError("Health is not set");
        }
    }

    public void OnPlayerInstaceCreated(PlayerStats player)
    {
        try
        {
            // 1 / 4
            UserOrderText.text = player.Order.ToString() + "/" + Enum.GetNames(typeof(Players)).Length.ToString();
        }
        catch (Exception e)
        {
            dummy = e.Message;
            //Debug.LogError("Order is not set");
        }
    }
}
