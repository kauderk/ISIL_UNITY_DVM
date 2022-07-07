using UnityEngine;

public class BulletController : MonoBehaviour
{
    private GameObject Target = null;
    
    private float speed = 30f;
    private float distance = 0f;
    [HideInInspector] public float thisDamage = 0f;

    private Vector3 direccion = Vector3.zero;

    private PlayerController.TYPEWEAPON weapon;

    private void Awake()
    {
        Target = GameObject.Find("Player");

        weapon = Target.GetComponent<PlayerController>().weapon;

        this.transform.position = Target.transform.position;
    }
    void Start() => direccion = Target.transform.forward + new Vector3(Random.Range(-0.3f, 0.3f), 0, Random.Range(-0.3f, 0.3f));

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

    private void BulletConfig(float limitDistance, float damage)
    {
        thisDamage = damage;
        distance = Vector3.Distance(Target.transform.position, this.transform.position);

        this.transform.position += speed * Time.deltaTime * direccion;

        if (distance > limitDistance)
        {
            Destroy(this.gameObject);
            //this.transform.position = Target.transform.position;
        }
    }
}

