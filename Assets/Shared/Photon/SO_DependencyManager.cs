using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;

[CreateAssetMenu(fileName = "DependencyManager", menuName = "Managers/DependencyManager", order = 1)]
public class SO_DependencyManager : SingletonScriptableObject<SO_DependencyManager>
{
    public GameObject playerPrefab;
    public GameObject cameraFollowPrefab;
    public void CreatePlayer()
    {
        var cam = InstantiateCamera();
        var player = InstantiatePlayer();

        cam.controller.AssignTarget(player.transform);
        player.transform.NotifyChildren<ICameraEvents>(I => I.OnCameraAnimatorChange(cam.animtor));

        new List<GameObject> { cam.go, player }
        .ForEach(go => go.SetActive(true));
    }

    private GameObject InstantiatePlayer()
    {
        var random = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        var player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(random.x, 12f, random.y), Quaternion.identity);
        player.SetActive(false);
        return player;
    }

    private (GameObject go, ICamera controller, Animator animtor) InstantiateCamera()
    {
        var go = PhotonNetwork.Instantiate(cameraFollowPrefab.name, new Vector3(0f, 20f, -20f), Quaternion.identity);
        go.SetActive(false);
        var controller = go.GetComponent<ICamera>();
        var animtor = go.GetComponent<Animator>();
        return (go, controller, animtor);
    }
}
