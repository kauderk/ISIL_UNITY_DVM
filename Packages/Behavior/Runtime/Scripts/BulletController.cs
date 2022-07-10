using UnityEngine;

public class BulletController : MonoBehaviour
{    
    private float speed = 30f;
    private float distance = 0f;
    [HideInInspector] public float thisDamage = 0f;

    private Vector3 direccion = Vector3.zero;

    private PlayerController.TYPEWEAPON weapon;

    private GameObject player = null;

    void Update()
    {
        switch (weapon)
        {
            case PlayerController.TYPEWEAPON.PISTOL:
                BulletConfig(20f,10f);
                break;
            case PlayerController.TYPEWEAPON.RIFLE:
                BulletConfig(30f,20f);
                break;
            case PlayerController.TYPEWEAPON.SHOTGUN:
                BulletConfig(10f,15f);
                break;
        }
    }

    public void Init(GameObject _player, Transform _inicialPos)
    {
        player = _player;
        
        weapon = _player.GetComponent<PlayerController>().weapon;

        this.transform.position = _inicialPos.transform.position;

        direccion = _inicialPos.transform.forward + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));
    }

    private void BulletConfig(float limitDistance, float damage)
    {
        thisDamage = damage;
        distance = Vector3.Distance(player.transform.position, this.transform.position);

        this.transform.position += speed * Time.deltaTime * direccion;

        if (distance > limitDistance)
        {
            Destroy(this.gameObject);
            //this.transform.position = Target.transform.position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var otherHealthComponent = other.gameObject.GetComponent<HealthController>();
            if (otherHealthComponent)
            {
                otherHealthComponent.GetDamage(thisDamage);
            }   
        }
    }
}

