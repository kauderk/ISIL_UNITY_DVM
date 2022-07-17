using EventBusSystem;
using UnityEngine;
using Weapon;

public interface ICollisionSubscriber : IGlobalSubscriber
{
    void OnCollisionWithMagazine(Collision collision);
}