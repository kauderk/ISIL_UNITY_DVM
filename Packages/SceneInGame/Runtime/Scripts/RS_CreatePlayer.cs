using UnityEngine;
using Photon.Pun;

public class RS_CreatePlayer : MonoBehaviourPun
{
    [SerializeField] private GameObject tankGogo;

    private void Start()
    {
        //Esta(() => CreatePlayer());
        //LauncherNW.Instance.OnJoinLobbyAction += CreatePlayer;
    }

    public void CreatePlayer()
    {
        //Unity debugger p?
        var p = PhotonNetwork.Instantiate(tankGogo.name, new Vector3(0f, 12f, 0f), Quaternion.identity);
        //PhotonNetwork.Instantiate(tank.name + "(Clone)", this.transform.position, Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) CreatePlayer();
    }

}
