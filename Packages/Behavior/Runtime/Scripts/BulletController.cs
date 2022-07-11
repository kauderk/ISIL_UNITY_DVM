using System;
using System.Collections;
using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// If no one sets a value, "settings.damage" will be used.
    /// </summary>
    [HideInInspector] public float? thisDamage = null;
    private SO_BulletSettings settings;
    // Stopwatch timer = new Stopwatch();
    public GameObject hitTarget = null;
    float rotSpeed;

    public Vector3 DesireedPosition;
    public float BulletSpeed = 0f;



    void Update() => Move();

    public void Init(SO_BulletSettings _settings) // Awake()
    {
        settings = ScriptableObject.Instantiate(_settings);
        settings.Init(_settings);
        transform.position = settings.origin;
        thisDamage = thisDamage ?? settings.damage;
        gameObject.transform.forward = settings.forward;
        StartCoroutine(Shoot());
        // timer.Start();
    }

    public void Move()
    {

        if (hitTarget)
        {
            var desiredDirection = hitTarget.transform.position - transform.position;
            var length = desiredDirection.magnitude;
            desiredDirection.Normalize();

            float angle = Vector3.SignedAngle(transform.forward, desiredDirection, transform.up);
            angle = Mathf.Abs(angle);

            float newAngle = Mathf.Lerp(Quaternion.Euler(transform.forward).y, angle, Time.deltaTime * 3f * rotSpeed);
            transform.Rotate(0, newAngle, 0);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, DesireedPosition, Time.deltaTime * BulletSpeed);
            // var direccion = transform.forward; //+ new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
            // var distance = Vector3.Distance(settings.caster.transform.position, transform.position);
            // transform.position += settings.speed * Time.deltaTime * direccion;

            // // rotate slightly to the left
            // //if (hitTarget == null)
            // transform.Rotate(0, UnityEngine.Random.Range(.5f, 3f), 0);
        }
    }

    private IEnumerator Shoot()
    {
        rotSpeed = 1f;
        while (true)
        {
            rotSpeed = rotSpeed == 0.75f ? 1.0f : 0.75f;
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void GetHitTarget()
    {
        RaycastHit HitRaycast;
        var hit = Physics.CapsuleCast(transform.position, transform.position + transform.forward * 2f, 2f, transform.forward, out HitRaycast, settings.layerMask);
        if (hit && HitRaycast.transform.gameObject && HitRaycast.transform.gameObject.tag == "Enemy")
        {
            hitTarget = HitRaycast.transform.gameObject;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            OnCustomDestroy();
            Destroy(gameObject);
        }
    }

    private void OnCustomDestroy()
    {
        settings.caster.GetComponent<IFireBullet>().RegenerateBulletList(this);
    }

    public static float Remap(float value, float from1, float from2, float to1, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
