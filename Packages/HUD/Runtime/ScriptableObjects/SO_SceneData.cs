
using System.Collections.Generic;
using UnityEngine;


public abstract class ISObject : ScriptableObject
{
    public string Name;
}

[ExecuteInEditMode]
[CreateAssetMenu(fileName = "New Scene Data", menuName = "Scriptable Objects/SceneData")]
public class SO_SceneData : ISObject
{
    public Sprite LoadingScreen;
    public SceneReference Scene;
    public List<string> additiveScenes = new List<string>();

    public async void Enter()
    {
        // get name form scene path
        var name = LoaderUtilsStatic.getSceneName(Scene);
        await SceneController.Instance.LoadScene(name);
    }

    void Update()
    {
        Debug.Log("UpdateMenuItemOnClick");
    }
}
