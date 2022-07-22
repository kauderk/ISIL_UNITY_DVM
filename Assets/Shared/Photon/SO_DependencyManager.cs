using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using EventBusSystem;
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
    //public GameObject cameraFollowPrefab;

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
        var player = InstantiatePlayer();
        player.go.transform.NotifyChildren<IPlayerStatsSubscriber>(I => I.OnStatsChanged(CreatePlayerStats(player.go)));
        player.go.SetActive(true); // show others regarless of their photon view
    }

    #region Query
    PlayerStats CreatePlayerStats(GameObject player)
    {
        var stats = GameplayManager.Instance.RequestNextPlayer();
        stats.Instance.Player = player;
        EventBus.RaiseEvent<IMultiplayerSubscriber>(I => I.OnPlayerInstaceCreated(stats));
        return stats;
    }
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