using UnityEngine;

public class LoaderUtils : MonoBehaviour
{

}

public static class LoaderUtilsStatic
{
    public static string getSceneName(SceneReference Scene)
    {
        var raw = Scene.ScenePath.Split('/')[Scene.ScenePath.Split('/').Length - 1];
        return raw.Substring(0, raw.Length - 6);
    }
}
