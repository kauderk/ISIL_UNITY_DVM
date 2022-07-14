using UnityEngine;

public class RS_CameraController : MonoBehaviour
{
    [Tooltip("Follow the Target with smoothness"), Range(0.01f, 1.0f)]
    public float Smoothness = 0.1f;

    [SerializeField] Vector3 distance = new Vector3(0, 10, -10);
    [SerializeField] Transform Target;

    public void AssignTarget(Transform target) => Target = target;
    void Update()
    {
        // follow the Target with smoothness
        transform.position = Vector3.Lerp(transform.position, Target.position + distance, Smoothness);
    }
}
