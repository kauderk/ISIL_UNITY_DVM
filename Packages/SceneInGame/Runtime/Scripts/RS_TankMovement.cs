using UnityEngine;
using Photon.Pun;

namespace Photon.Pun
{
    public class RS_TankMovement : MonoBehaviourPunBase
    {
        public SO_PlayerSettings Settings;
        float rotY, dirZ;

        protected override void MyUpdate()
        {
            if (!photonView.IsMine && PhotonNetwork.IsConnected)
                return;
            rotY = Input.GetAxis("Horizontal") * Settings.rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotY, 0);

            dirZ = Input.GetAxis("Vertical") * Settings.movementSpeed * Time.deltaTime;
            transform.Translate(0, 0, dirZ);
        }
    }
}
