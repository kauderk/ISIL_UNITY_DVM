using EventBusSystem;
using UnityEngine;
using Weapon;

public class KD_CollisionController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.TryGetComponent<IMagazine>(out var magazine))
        {
            collision.gameObject.SetActive(false); // hardcoded FIXME: use a pool
            EventBus.RaiseEvent<ICollisionSubscriber>(I => I.OnCollisionWithMagazine(collision));
        }
    }
}
