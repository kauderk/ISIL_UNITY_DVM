using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform Target = null;

    private Vector3 distance = Vector3.zero;

    private void Awake()
    {
        distance = new Vector3(0, 15, -20);
    }
    void Update()
    {
        this.transform.position = Target.transform.position + distance;
    }
}
