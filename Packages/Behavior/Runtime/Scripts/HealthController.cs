using UnityEngine;

public class HealthController : MonoBehaviour
{
    private float health = 100f;

    public void GetDamage(float damage)
    {
        health -= damage;
        if (health <= 0) this.gameObject.SetActive(false);
    }
}
