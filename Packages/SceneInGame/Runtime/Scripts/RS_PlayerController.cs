using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RS_PlayerController : MonoBehaviour
{
    PhotonView photonView;
    void Start()
    {
        if (photonView.IsMine)
        {
            Debug.Log("isMine");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
