using UnityEngine.SceneManagement;

public static class UnitySceneUtils
{
    /// <summary>
    /// Returns true if the scene 'name' exists and is in your Build settings, false otherwise
    /// </summary>
    public static string DoesSceneExist(object o)
    {
        // https://gist.github.com/yagero/2cd50a12fcc928a6446539119741a343
        var name = "";
        if (o.GetType() == typeof(string))
            name = (string)o;
        else if (o.GetType() == typeof(int))
            name = GetSceneNameByIndex((int)o);
        else if (o.GetType() == typeof(SceneReference)) // SOLID!!!
            name = GetSceneNameFromPath(((SceneReference)o).ScenePath);
        else if (o.GetType() == typeof(SceneID)) // SOLID!!!
            name = GetSceneNameByIndex((int)o);
        else
            return null;

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string sceneName = GetSceneNameByIndex(i);

            if (string.Compare(name, sceneName, true) == 0)
                return name;
        }

        return null;
    }

    private static string GetSceneNameByIndex(int i)
    {
        var scenePath = SceneUtility.GetScenePathByBuildIndex(i);
        return GetSceneNameFromPath(scenePath);
    }

    private static string GetSceneNameFromPath(string scenePath)
    {
        var lastSlash = scenePath.LastIndexOf("/");
        return scenePath.Substring(lastSlash + 1, scenePath.LastIndexOf(".") - lastSlash - 1);
    }

    public static int sceneIndexFromName(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string testedScreen = NameFromIndex(i);
            //print("sceneIndexFromName: i: " + i + " sceneName = " + testedScreen);
            if (testedScreen == sceneName)
                return i;
        }
        return -1;
    }
    public static string NameFromIndex(int BuildIndex)
    {
        string path = SceneUtility.GetScenePathByBuildIndex(BuildIndex);
        int slash = path.LastIndexOf('/');
        string name = path.Substring(slash + 1);
        int dot = name.LastIndexOf('.');
        return name.Substring(0, dot);
    }
}