using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// If no one sets a value, "settings.damage" will be used.
    /// </summary>
    [HideInInspector] public float? thisDamage = null;
    private SO_AmmoSettings settings;

    void Update() => Move();

    public void Init(SO_AmmoSettings _settings) // Awake()
    {
        settings = SO_AmmoSettings.Instantiate(_settings); // create a new instance to avoid changing the original settings, because c# passes by reference
        transform.position = settings.Instance.origin.transform.position;
        thisDamage ??= settings.damage;
    }

    float RandomJiggle() => Random.Range(settings.jigle.x, settings.jigle.y);
    private void Move()
    {
        var direccion = settings.originForward + new Vector3(RandomJiggle(), 0, RandomJiggle());
        var distance = Vector3.Distance(settings.Instance.caster.transform.position, transform.position);
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
