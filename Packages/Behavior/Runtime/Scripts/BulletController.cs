using System.Diagnostics;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 30f;
    private float distance = 0f;
    [HideInInspector] public float thisDamage = 0f;

    private Vector3 direccion = Vector3.zero;

    private PlayerController.TYPEWEAPON weapon = PlayerController.TYPEWEAPON.PISTOL;

    private GameObject player = null;
    // why is this here?
    [SerializeField] private Transform scope = null;



    void Update() => SwitchBulletFlowOnWeaponType();

    private void SwitchBulletFlowOnWeaponType()
    {
        switch (weapon)
        {
            case PlayerController.TYPEWEAPON.PISTOL:
                _BulletDistanceAndDamage(20f, 10f);
                break;
            case PlayerController.TYPEWEAPON.RIFLE:
                _BulletDistanceAndDamage(30f, 20f);
                break;
            case PlayerController.TYPEWEAPON.SHOTGUN:
                _BulletDistanceAndDamage(10f, 15f);
                break;
        }
    }

    void _BulletDistanceAndDamage(float dis, float dmg)
    {
        if (!player)
            Debugger.Break();
        BulletConfig(dis, dmg, player, player.transform.Find("Scope"));
    }

    public void Init(GameObject _player, Transform _scope = null)
    {
        player = _player;
        scope = _scope;
        if (!player)
            Debugger.Break();
        //AWAKE();
        SwitchBulletFlowOnWeaponType();
    }

    private void BulletConfig(float limitDistance, float damage, GameObject _player, Transform _inicialPos)
    {
        player = _player;

        weapon = _player.GetComponent<PlayerController>().weapon;

        this.transform.position = _inicialPos.transform.position;

        direccion = _inicialPos.transform.forward + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));


        thisDamage = damage;
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        this.transform.position += speed * Time.deltaTime * direccion;

        if (distance > limitDistance)
        {
            Debugger.Break();
            Destroy(this.gameObject);
            //this.transform.position = Target.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<KD_IDamage>(out var damage))
        {
            damage.TakeDamage(thisDamage);
        }
    }
}
