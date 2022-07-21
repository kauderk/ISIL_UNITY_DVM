using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "DependencyManager", menuName = "Managers/DependencyManager", order = 1)]
public class SO_DependencyManager : SingletonScriptableObject<SO_DependencyManager>
{
    [Tooltip("Should Only be used for testing purposes")]
    public bool CreatePlayerOffline = false;
    public GameObject playerPrefab;
    public GameObject cameraFollowPrefab;

    private void OnEnable()
    {
        MainManager.RegisterOnGameInitialized(() =>
        {
            if (CreatePlayerOffline)
            {
                CreatePlayerPrefabs();
            }
        });
    }

    public void CreatePlayerPrefabs()
    {
        var cam = InstantiateCamera();
        var player = InstantiatePlayer();

        cam.controller.AssignTarget(player.transform);
        player.transform.NotifyChildren<ICameraEvents>(I => I.OnCameraAnimatorChange(cam.animtor));

        new List<GameObject>() { player, cam.go }
        .ForEach(go => go.SetActive(true));
    }

    #region Query
    private GameObject InstantiatePlayer()
    {
        var random = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        var player = Instantiate(playerPrefab, new Vector3(random.x, 12f, random.y), Quaternion.identity);
        player.SetActive(false);
        return player;
    }

    private (GameObject go, ICamera controller, Animator animtor) InstantiateCamera()
    {
        GameObject go = Instantiate(cameraFollowPrefab, new Vector3(0f, 20f, -20f), Quaternion.identity);
        go.SetActive(false);
        var controller = go.GetComponent<ICamera>();
        var animtor = go.GetComponentInChildren<Animator>(); //TODO: what's the convention?
        return (go, controller, animtor);
    }

    private GameObject Instantiate(GameObject prefabName, Vector3 position, Quaternion rotation)
    {
        if (CreatePlayerOffline)
            return Object.Instantiate(prefabName, position, rotation);
        else
            return PhotonNetwork.Instantiate(prefabName.name, position, rotation);
    }
    #endregion
}
