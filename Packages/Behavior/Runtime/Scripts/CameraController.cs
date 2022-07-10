using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks
{
    private Vector3 distance = Vector3.zero;
    private Transform myCamera = null;

    private void Awake()
    {
        distance = new Vector3(0, 15, -20);
        myCamera = GameObject.Find("Main Camera").transform;
    }
    void Update()
    {
        if (photonView.IsMine) myCamera.transform.position = this.transform.position + distance;
    }
}
