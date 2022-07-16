using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace Photon.Pun
{
    public class MonoBehaviourPunBase : MonoBehaviourPun
    {
        // Update is called once per frame
        void Update()
        {
            if (photonView.IsMine)
                MyUpdate();
        }
        protected virtual void MyUpdate() { }
    }
}