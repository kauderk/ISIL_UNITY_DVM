using UnityEngine;
using Photon.Pun;

namespace Photon.Pun
{
    public class RS_TankMovement : MonoBehaviourPunBase
    {
        public SO_PlayerSettings Settings;
        float rotY, dirZ;

        //public Camera camera = null;

        private PhotonView photonView = null;

        protected override void MyUpdate()
        {
            photonView = GetComponent<PhotonView>();

            if (this.photonView.IsMine)
            {
                rotY = Input.GetAxis("Horizontal") * Settings.rotationSpeed * Time.deltaTime;
                this.transform.Rotate(0, rotY, 0);

                dirZ = Input.GetAxis("Vertical") * Settings.movementSpeed * Time.deltaTime;
                this.transform.Translate(0, 0, dirZ);

                //this.camera.transform.position = Vector3.Lerp(this.camera.transform.position, this.photonView.transform.position, 0f);
            }
            else return;

            //if (!photonView.IsMine)
            //{
            //    this.camera.enabled = false;
            //}
        }
    }
}
