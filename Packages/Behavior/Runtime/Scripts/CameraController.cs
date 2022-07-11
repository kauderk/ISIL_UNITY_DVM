using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks
{
    private Vector3 distance = new Vector3(0, 15, -20);
    private Transform myCamera = null;

    private void Awake()
    {
        if(photonView.IsMine)
        {
            myCamera = GameObject.FindGameObjectWithTag("Camera").transform; 
            myCamera.transform.position = this.transform.position + distance;
        }

    }
    void Update()
    {
        if (photonView.IsMine) myCamera.transform.position = this.transform.position + distance;
    }
}
