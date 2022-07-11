using UnityEngine;

public class HealthController : MonoBehaviour, KD_IDamage
{
    private float health = 100f;

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Dead", gameObject);
            this.gameObject.SetActive(false);
        }
    }
}
