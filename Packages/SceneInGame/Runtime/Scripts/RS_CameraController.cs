using UnityEngine;

public class RS_CameraController : MonoBehaviour
{
    [SerializeField] private Transform Target = null;
    private Vector3 distance = Vector3.zero;

    private void Awake() => distance = new Vector3(0, 10, -10);

    void Update() => this.transform.position = Target.transform.position + distance;
}
