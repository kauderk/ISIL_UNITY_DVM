using UnityEngine;
using Photon.Pun;

public class RS_TankMovement : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings Settings;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * Settings.rotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * Settings.movementSpeed);
    }
}
