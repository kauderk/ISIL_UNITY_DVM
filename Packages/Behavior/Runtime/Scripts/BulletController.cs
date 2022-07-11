using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// If no one sets a value, "settings.damage" will be used.
    /// </summary>
    [HideInInspector] public float? thisDamage = null;
    private SO_BulletSettings settings;

    void Update() => Move();

    public void Init(SO_BulletSettings _settings) // Awake()
    {
        settings = ScriptableObject.Instantiate(_settings);

        transform.position = settings.origin.transform.position;
        thisDamage = thisDamage ?? settings.damage;
    }

    private void Move()
    {
        var direccion = settings.originPos + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
        var distance = Vector3.Distance(settings.caster.transform.position, transform.position);
        transform.position += settings.speed * Time.deltaTime * direccion;

        if (distance > settings.limitDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<KD_IDamage>(out var damage))
            damage.TakeDamage(thisDamage ?? settings.damage);
    }
}
