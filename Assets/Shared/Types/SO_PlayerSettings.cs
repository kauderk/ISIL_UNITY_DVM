using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game Data/PlayerSettings", order = 1)]
public class SO_PlayerSettings : ScriptableObject
{
    public float rotationSpeed = 200f;
    public float movementSpeed = 5.0f;
    public GameObject dustTrailBackPrefab;
    public GameObject dustTrailPrefab;
    public GameObject bullet;
    public TYPEWEAPON weapon;

    public Animator animator { get; private set; }
    public ParticleSystem dustTrail { get; private set; }
    public ParticleSystem dustTrailBack { get; private set; }

    public void Init(GameObject self)
    {
        animator = self.GetComponent<Animator>();
        dustTrail = Instantiate(dustTrailPrefab).GetComponent<ParticleSystem>();
        dustTrailBack = Instantiate(dustTrailBackPrefab).GetComponent<ParticleSystem>();
    }
}
