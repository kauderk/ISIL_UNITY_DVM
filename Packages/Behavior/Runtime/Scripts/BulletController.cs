using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 30f;
    private float distance = 0f;
    [HideInInspector] public float thisDamage = 0f;

    private Vector3 direccion = Vector3.zero;

    /// <summary>
    /// Using Shared Types: PISTOL, SHOTGUN, RIFLE
    /// </summary>
    private TYPEWEAPON weapon = TYPEWEAPON.PISTOL;

    /// <summary>
    /// GameObject that shot this bullet.
    /// </summary>
    private GameObject caster = null;

    /// <summary>
    /// The scope of the bullet.
    /// </summary>
    private Vector3 originPos = Vector3.zero;



    void Update() => SwitchBulletFlowOnWeaponType();

    private void SwitchBulletFlowOnWeaponType()
    {
        switch (weapon)
        {
            case TYPEWEAPON.PISTOL:
                _BulletDistanceAndDamage(20f, 10f, weapon);
                break;
            case TYPEWEAPON.RIFLE:
                _BulletDistanceAndDamage(30f, 20f, weapon);
                break;
            case TYPEWEAPON.SHOTGUN:
                _BulletDistanceAndDamage(10f, 15f, weapon);
                break;
        }
    }

    void _BulletDistanceAndDamage(float dis, float dmg, TYPEWEAPON weapon)
    {
        BulletConfig(dis, dmg, weapon);
    }

    public void Init(GameObject _caster, Transform _origin = null)
    {
        caster = _caster;
        //AWAKE();
        transform.position = _origin.transform.position;
        originPos = _origin.transform.forward;
        // SwitchBulletFlowOnWeaponType(); will be called in Update() anyways
    }

    private void BulletConfig(float limitDistance, float damage, TYPEWEAPON _weapon)
    {
        weapon = _weapon;

        direccion = originPos + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));

        thisDamage = damage;
        distance = Vector3.Distance(caster.transform.position, transform.position);

        transform.position += speed * Time.deltaTime * direccion;

        if (distance > limitDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<KD_IDamage>(out var damage))
            damage.TakeDamage(thisDamage);
    }
}
