using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public interface IMenuData
{
    string GetRoomName();
    string SetNickname();
}

public class KD_PlayerUIStats : MonoBehaviourPunBase //, IMenuData
{
    private void Start()
    {
        GetComponent<TMP_Text>().text = PhotonNetwork.NickName;
    }
}
