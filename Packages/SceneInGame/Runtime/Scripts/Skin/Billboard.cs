using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;
using Photon.Pun;

public class Billboard : MonoBehaviourPunBase, ICamera
{
    public Transform cam; Quaternion rotQuat;

    // private void OnEnable() => EventBus.Subscribe(this);
    // private void OnDisable() => EventBus.Unsubscribe(this);
    private void Start() => rotQuat = transform.rotation;
    public void OnCameraUpdate(Transform camera) => cam = camera;
    private void Awake() {
        cam = cam == null ? Camera.main.transform : cam;
    }
    public void AssignTarget(Transform target)
    {
        cam = target;
    }
    public void AssignTarget(int id)
    {
        cam = PhotonView.Find(id).transform;
    }
    public Transform GetTarget()
    {
        return cam;
    }

    void Update()
    {
        if (cam == null) return;
        //transform.LookAt(cam.transform);
        //transform.rotation = Quaternion.LookRotation(cam.transform.position - transform.position);
        transform.rotation = cam.transform.rotation * rotQuat;
    }
}
