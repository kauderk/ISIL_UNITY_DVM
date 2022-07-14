using UnityEngine;
using Photon.Pun;

public class CameraController : MonoBehaviourPunCallbacks
{
    [field: SerializeField]
    public Transform myCamera { get; private set; }

    Vector3 distance = new Vector3(0, 15, -20);

    private void Awake()
    {
        if (!photonView.IsMine)
        {
            Debug.Log("CameraController: Awake: Not mine... disabling");
            enabled = false;
        }

        myCamera = GameObject.FindGameObjectWithTag("Camera").transform; //FIXME: 
        myCamera.transform.position = transform.position + distance;
    }
    void Update()
    {
        // follow the player 
        myCamera.transform.position = transform.position + distance;
    }
}
