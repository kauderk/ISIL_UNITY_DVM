using EventBusSystem;
using UnityEngine;
using Visual;
using Weapon;

public class KD_SkinMono : WeaponMonoBehaviourPunBase, ICollisionSubscriber // FIXME: don't repeat yourself
{
    public SO_WeaponSkin Settings;
    public bool ChangeOnStart = true;
    public bool random;
    public Color Color;

    private void OnEnable() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);

    public void Awake()
    {
        if (ChangeOnStart)
            NotifySiblings();
    }
    public void NotifySiblings()
    {
        transform.NotifySiblings<ISkin>(I => I.ApplyColor(Settings.Color.Runtime));
    }

    public void OnCollisionWithMagazine(Collision collision)
    {
        var magazine = collision.gameObject.GetComponent<IMagazine>();
        Settings.Color.Runtime = magazine.skinSettings.Color.Editor;
        NotifySiblings();
    }
}
