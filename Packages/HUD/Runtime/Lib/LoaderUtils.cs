using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderUtils : MonoBehaviour
{
    public ScriptableObject scriptableObject;

    public async void LoadScene(SwipeMenu.Menu MenuRef)
    {
        var sceneIndex = MenuRef.getCurrentItem();
        var obj = MenuRef.gameObject.transform.GetChild(sceneIndex);
        var loader = obj.GetComponent<LoaderUtils>();
        var data = (SO_SceneData)loader.scriptableObject;
        var id = LoaderUtilsStatic.getSceneName(data.Scene);
        await SceneController.Instance.LoadScene(id);
    }
    public async void LoadScene(int index)
    {
        await SceneController.Instance.LoadScene(index);
    }
    public async void LoadScene(string nameOrPath)
    {
        await SceneController.Instance.LoadScene(nameOrPath);
    }
    public async void LoadScene(LoaderUtils loader)
    {
        var data = (SO_SceneData)loader.scriptableObject;
        var id = LoaderUtilsStatic.getSceneName(data.Scene);
        await SceneController.Instance.LoadScene(id);
    }

}

public static class LoaderUtilsStatic
{
    public static string getSceneName(SceneReference Scene)
    {
        var raw = Scene.ScenePath.Split('/')[Scene.ScenePath.Split('/').Length - 1];
        return raw.Substring(0, raw.Length - 6);
    }
}
