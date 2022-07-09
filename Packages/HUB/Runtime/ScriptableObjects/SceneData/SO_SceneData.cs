
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class ISObject : ScriptableObject
{
    public string Name;
}

[System.Serializable]
[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Scene Data", menuName = "Scriptable Objects/SceneData")]
public class SO_SceneData : ISObject
{
    public Sprite LoadingScreen;
    public SceneReference Scene;
    public List<string> additiveScenes = new List<string>();
    [SerializeField]
    public SceneID sceneID;
    [SerializeField]
    public Sprite menuItemCover;

    public async void Enter()
    {
        await SO_SceneLoader.Instance.LoadScene(sceneID);
    }
}
