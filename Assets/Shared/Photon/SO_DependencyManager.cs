using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    PhotonView myPhotonView;

    public void CreatePlayerPrefabs()
    {
        var cam = InstantiateCamera();
        var player = InstantiatePlayer();

        cam.controller.AssignTarget(player.go.transform);
        player.go.transform.NotifyChildren<ICameraEvents>(I => I.OnCameraAnimatorChange(cam.animtor));
        player.go.transform.NotifyChildren<IPlayerStatsSubscriber>(I => I.OnStatsChanged(CreatePlayerStats(player.go, cam.go)));

        player.go.SetActive(true); // show others regarless of their photon view
        cam.go.SetActive(CreatePlayerOffline || cam.photonView.IsMine); // if set to offline, show camera regardless of photon view
    }
    PlayerStats CreatePlayerStats(GameObject player, GameObject cam)
    {
        var stats = new PlayerStats();
        stats.Instance.Player = player;
        stats.Instance.Camera = cam;
        return stats;
    }

    #region Query
    private PlayerInstace InstantiatePlayer()
    {
        var random = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5)); // FIXME:
        var go = Instantiate(playerPrefab, new Vector3(random.x, 12f, random.y), Quaternion.identity);
        go.SetActive(false);
        return new PlayerInstace()
        {
            go = go,
            photonView = go.GetComponent<PhotonView>()
        };
    }

    private CamInstace InstantiateCamera()
    {
        GameObject go = Instantiate(cameraFollowPrefab, new Vector3(0f, 20f, -20f), Quaternion.identity);
        go.SetActive(false);
        return new CamInstace()
        {
            go = go,
            controller = go.GetComponent<ICamera>(),
            animtor = go.GetComponentInChildren<Animator>(), //TODO: what's the convention?
            photonView = go.GetComponent<PhotonView>(),
        };
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
public class CamInstace
{
    public GameObject go;
    public ICamera controller;
    public Animator animtor;
    public PhotonView photonView;
}
public class PlayerInstace
{
    public GameObject go;
    public PhotonView photonView;
}

// create a custom editor for this scriptable object
#if UNITY_EDITOR
[CustomEditor(typeof(SO_DependencyManager))]
public class SO_DependencyManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Create Player"))
        {
            ((SO_DependencyManager)target).CreatePlayerPrefabs();
        }
    }
}
#endif