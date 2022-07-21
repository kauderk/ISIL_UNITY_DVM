using UnityEngine;
using Photon.Pun;

public class RS_CameraController : MonoBehaviourPunBase, ICamera
{
    [Tooltip("Follow the Target with smoothness"), Range(0.01f, 1.0f)]
    public float Smoothness = 0.1f;

    [SerializeField] Vector3 distance = new Vector3(0, 10, -10);
    [SerializeField] Transform Target;

    public void AssignTarget(Transform target) => Target = target;
    public void AssignTarget(int id)
    {
        Target = PhotonView.Find(id).transform;
    }
    public Transform GetTarget() => Target;

    protected override void MyUpdate()
    {
        // follow the Target with smoothness
        if (Target)
            transform.position = Vector3.Lerp(transform.position, Target.position + distance, Smoothness);
    }
}
