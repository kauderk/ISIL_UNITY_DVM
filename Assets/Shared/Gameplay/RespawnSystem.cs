using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem Instance; // singleton

    [Header("Points")]
    public Transform ParentWithChildernPoints;
    List<Transform> points;

    [Header("Spawn")]
    public static List<float> SpawnTimes = new List<float>() { 1f, 2f, 4f, 6f };
    public static float GetSpawnTime(int deathCount) => SpawnTimes[deathCount];


    private void Awake()
    {
        points = ParentWithChildernPoints.GetComponentsInChildren<Transform>().ToList();
    }

    public Transform GetRandomPoint()
    {
        var index = Random.Range(0, points.Count);
        return points[index];
    }

    public void CallRespawnPlayer(PlayerStats stats) => StartCoroutine(RespawnPlayer(stats));
    public IEnumerator RespawnPlayer(PlayerStats ID)
    {
        yield return new WaitForSeconds(2); // wait
        // create
        var creation = SO_DependencyManager.Instance.CreatePlayerPrefabs();
        // set random position
        creation.player.transform.position = GetRandomPoint().position;
        // enable gameobjects
        creation.list.ForEach(go => go.SetActive(true));
    }

    void OnEnable()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
