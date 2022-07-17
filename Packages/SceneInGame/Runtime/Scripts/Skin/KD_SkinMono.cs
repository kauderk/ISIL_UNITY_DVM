using EventBusSystem;
using UnityEngine;
using Visual;
using Weapon;

public class KD_SkinMono : WeaponMonoBehaviourPunBase // FIXME: don't repeat yourself
{
    public SO_WeaponSkin Settings;
    public bool ChangeOnStart = true;
    public bool random;
    public Color Color;

    public void Awake()
    {
        if (ChangeOnStart)
            NotifySiblings();
    }
    public void NotifySiblings()
    {
        transform.NotifySiblings<ISkin>(I => I.ApplyColor(Settings.Color.Runtime));
    }


}
