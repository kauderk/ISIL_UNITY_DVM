using UnityEngine;

public class BulletController : MonoBehaviour
{    
    private float speed = 30f;
    private float distance = 0f;
    [HideInInspector] public float thisDamage = 0f;

    private Vector3 direccion = Vector3.zero;

    private PlayerController.TYPEWEAPON weapon = PlayerController.TYPEWEAPON.PISTOL;

    private GameObject player = null;

    private void Awake()
    {
        switch (weapon)
        {
            case PlayerController.TYPEWEAPON.PISTOL:
                BulletConfig(20f, 10f,player,player.transform.GetChild(5));
                break;
            case PlayerController.TYPEWEAPON.RIFLE:
                BulletConfig(30f, 20f,player, player.transform.GetChild(5));
                break;
            case PlayerController.TYPEWEAPON.SHOTGUN:
                BulletConfig(10f, 15f,player, player.transform.GetChild(5));
                break;
        }
    }

    void Update()
    {
        switch (weapon)
        {
            case PlayerController.TYPEWEAPON.PISTOL:
                BulletConfig(20f,10f, player, player.transform.GetChild(5));
                break;
            case PlayerController.TYPEWEAPON.RIFLE:
                BulletConfig(30f,20f, player, player.transform.GetChild(5));
                break;
            case PlayerController.TYPEWEAPON.SHOTGUN:
                BulletConfig(10f,15f, player, player.transform.GetChild(5));
                break;
        }
    }

    public void Init(GameObject _player, Transform _inicialPos)
    {
        
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
            Destroy(this.gameObject);
            //this.transform.position = Target.transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var otherHealthComponent = collision.gameObject.GetComponent<HealthController>();
            if (otherHealthComponent) otherHealthComponent.GetDamage(thisDamage);
        }
    }
}

