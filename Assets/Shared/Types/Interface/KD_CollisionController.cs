using EventBusSystem;
using UnityEngine;
using Weapon;

public class KD_CollisionController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IMagazine>(out var magazine))
        {
            EventBus.RaiseEvent<ICollisionSubscriber>(I => I.OnCollisionWithMagazine(magazine, collision));
            Destroy(collision.gameObject);
        }
    }
}
