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
    public IEnumerator RespawnPlayer(PlayerStats stats)
    {
        // TODO: implement state Handler, to react to OnDead() and OnTryingToRespawn()
        // currently it's better to move right away and avoid taking extra damage
        stats.Instance.Player.SetActive(false);
        stats.Instance.Player.transform.position = GetRandomPoint().position;

        yield return new WaitForSeconds(stats.GetSpawnTime());
        stats.OnDead(); // the order to get the respawn time is important

        var canRespawn = stats.OnTryingToRespawn();
        stats.Instance.Player.SetActive(canRespawn);
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
