using UnityEngine;
using Photon.Pun;

public class RS_CreatePlayer : MonoBehaviour
{
    [SerializeField] private GameObject tankGogo;
    [SerializeField] private Camera playerCamera;
    void Start()
    {
        GameObject tank = Instantiate(tankGogo);
        PhotonNetwork.Instantiate(tank.name, Vector3.zero, Quaternion.identity);        
    }
}
