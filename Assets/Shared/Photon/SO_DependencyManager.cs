using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "DependencyManager", menuName = "Managers/DependencyManager", order = 1)]
public class SO_DependencyManager : SingletonScriptableObject<SO_DependencyManager>
{
    public GameObject playerPrefab;
    public GameObject cameraFollowPrefab;
    public void CreatePlayer()
    {
        var cam = PhotonNetwork.Instantiate(cameraFollowPrefab.name, new Vector3(0f, 20f, -20f), Quaternion.identity);
        cam.SetActive(false);
        var camController = cam.GetComponent<RS_CameraController>();

        var player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 12f, 0f), Quaternion.identity);
        player.SetActive(false);

        camController.AssignTarget(player.transform);

        player.transform.NotifyChildren<ICameraEvents>(I => I.OnCameraAnimatorChange(cam.GetComponentInChildren<Animator>()));

        player.SetActive(true);
        cam.SetActive(true);
    }
    public void Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(prefab.name, position, rotation);
    }
}
