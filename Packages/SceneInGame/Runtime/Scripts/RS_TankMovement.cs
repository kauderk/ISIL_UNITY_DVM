using UnityEngine;
using Photon.Pun;

namespace Photon.Pun
{
    public class RS_TankMovement : MonoBehaviourPunBase
    {
        public SO_PlayerSettings Settings;

        protected override void MyUpdate()
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * Settings.rotationSpeed, 0);
            transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * Settings.movementSpeed);
        }
    }
}
