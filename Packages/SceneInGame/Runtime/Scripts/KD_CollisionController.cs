using EventBusSystem;
using UnityEngine;
using Weapon;

public class KD_CollisionController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<KD_Magazine>(out var magazine))
        {
            var weaponSettings = magazine.WeaponSettings;
            EventBus.RaiseEvent<ICollisionSubscriber>(I => I.OnCollisionWithMagazine(weaponSettings, collision));
            Destroy(collision.gameObject);
        }
    }
}
