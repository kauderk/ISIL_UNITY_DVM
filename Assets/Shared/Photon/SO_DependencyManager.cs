using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

[CreateAssetMenu(fileName = "DependencyManager", menuName = "Managers/DependencyManager", order = 1)]
public class SO_DependencyManager : SingletonScriptableObject<SO_DependencyManager>
{
    public GameObject playerPrefab;
    public void CreatePlayer()
    {
        PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0f, 12f, 0f), Quaternion.identity);
    }
    public void Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        PhotonNetwork.Instantiate(prefab.name, position, rotation);
    }
}
