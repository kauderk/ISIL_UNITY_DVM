using UnityEngine;

namespace Weapon
{
    public interface IMagazine
    {
        SO_WeaponMagazine Settings { get; }
        SO_WeaponSkin SkinSettings { get; }
        void PickUp();
        (SO_AmmoSettings ammo, SO_WeaponSkin skin) AmmoAndVisualOnPickup(IMagazine magazine); // self
    }
}
