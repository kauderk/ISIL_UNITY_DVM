using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    /// <summary>
    /// If no one sets a value, "settings.damage" will be used.
    /// </summary>
    [HideInInspector] public float? thisDamage = null;
    private SO_BulletSettings settings;
    Stopwatch timer = new Stopwatch();
    private GameObject hitTarget = null;

    void Update() => Move();

    public void Init(SO_BulletSettings _settings) // Awake()
    {
        settings = ScriptableObject.Instantiate(_settings);
        settings.Init(_settings);
        transform.position = settings.origin;
        thisDamage = thisDamage ?? settings.damage;
        gameObject.transform.forward = settings.forward;
        timer.Start();
    }

    public static float Remap(float value, float from1, float from2, float to1, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

    private void Move()
    {
        // destroy
        if (timer.ElapsedMilliseconds > 3000)
        {
            //Destroy(gameObject);
        }
        // move
        if (hitTarget == null)
            GetHitTarget();
        else
        {
            var desiredDirection = hitTarget.transform.position - transform.position;
            var length = desiredDirection.magnitude;
            float rotSpeed = Remap(length, 0, Mathf.Clamp(length, 0, 10), 10, 1);
            desiredDirection.Normalize();

            // convert vector3 to rotation
            // var rotation = Quaternion.LookRotation(desiredDirection);
            float angle = Vector3.SignedAngle(transform.forward, desiredDirection, transform.up);

            float newAngle = Mathf.Lerp(Quaternion.Euler(transform.forward).y, angle, Time.deltaTime * 3f * rotSpeed);
            transform.Rotate(0, newAngle, 0);
        }


        var direccion = transform.forward; //+ new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
        var distance = Vector3.Distance(settings.caster.transform.position, transform.position);
        transform.position += settings.speed * Time.deltaTime * direccion;

        // rotate slightly to the left
        if (hitTarget == null)
            transform.Rotate(0, Random.Range(.5f, 3f), 0);
        // draw a line to the forward direction
        UnityEngine.Debug.DrawLine(transform.position, transform.position + transform.forward * 50, Color.red);
        //if (distance > settings.limitDistance)
        //Destroy(gameObject);
    }

    private void GetHitTarget()
    {
        RaycastHit HitRaycast;
        // capsule cast to detect if there is something in the way
        var hit = Physics.CapsuleCast(transform.position, transform.position + transform.forward * 2f, 1f, transform.forward, out HitRaycast, settings.layerMask);


        if (hit && HitRaycast.transform.gameObject && HitRaycast.transform.gameObject.tag == "Enemy")
        {
            //UnityEngine.Debug.Log("Hit Found: " + HitRaycast.transform.gameObject.name);
            hitTarget = HitRaycast.transform.gameObject;

        }
        // debug capsule cast
        UnityEngine.Debug.DrawLine(transform.position, transform.position + transform.forward * 5f, Color.green);
    }

    public void OnCollisionEnter(Collision other)
    {
        UnityEngine.Debug.Log("Collision: " + other.gameObject.tag);
        if (other.gameObject.tag == "Enemy")
        {
            // destroy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        UnityEngine.Debug.Log("Collider: " + other.gameObject.tag);
        // if other's tag
        if (other.gameObject.tag == "Enemy")
        {
            // destroy
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
