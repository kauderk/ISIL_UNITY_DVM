using EventBusSystem;
using UnityEngine;
using Weapon;

public interface ICollisionSubscriber : IGlobalSubscriber
{
    void OnCollisionWithMagazine(SO_WeaponSettings weaponSettings, Collision collision);
}
