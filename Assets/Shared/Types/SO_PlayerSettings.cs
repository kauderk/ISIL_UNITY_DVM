using UnityEngine;

[System.Serializable]
public class KD_TankEditorSettings
{
    [field: SerializeField]
    public Animator animator { get; private set; }
    [field: SerializeField]
    public ParticleSystem dustTrail { get; private set; }
    [field: SerializeField]
    public ParticleSystem dustTrailBack { get; private set; }
}
[CreateAssetMenu(fileName = "PlayerSettings", menuName = "Game Data/PlayerSettings", order = 1)]
public class SO_PlayerSettings : ScriptableObject
{
    public float rotationSpeed = 200f;
    public float movementSpeed = 5.0f;
}
