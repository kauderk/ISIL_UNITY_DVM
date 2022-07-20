using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class RespawnSystem : MonoBehaviour
{
    public static RespawnSystem Instance; // singleton
    public List<GameObject> Players;
    public List<Transform> Points;

    // getRandomPoint
    public Transform GetRandomPoint()
    {
        var index = Random.Range(0, Points.Count);
        return Points[index];
    }
    public void CallRespawnPlayer()
    {
        StartCoroutine(RespawnPlayer());
    }
    // create a coroutine to respawn the player
    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(5); // wait
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
