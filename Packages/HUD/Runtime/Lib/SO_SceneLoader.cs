using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Loaders/SceneLoader")]
public class SO_SceneLoader : SingletonScriptableObject<SO_SceneLoader>
{
    public List<SO_SceneData> LoadingData = new List<SO_SceneData>();

    private static GameObject pupetMonoBehaviour;
    private void OnEnable()
    {
        MainManager.OnGameInitialized += OnLoad;
    }
    private void OnDisable()
    {
        MainManager.OnGameInitialized -= OnLoad;
        CleanUp();
    }
    private static void CleanUp()
    {
#if UNITY_EDITOR
        DestroyImmediate(pupetMonoBehaviour);
#else
        Destroy(pupetMonoBehaviour);
#endif
    }

    private void OnLoad()
    {
        pupetMonoBehaviour = new GameObject("Singleton_SceneLoader");
        pupetMonoBehaviour.AddComponent<LoaderUtils>();
        DontDestroyOnLoad(pupetMonoBehaviour);
    }
    public static void RequestSceneLoad(string name)
    {
        LoadScene(SO_SceneLoader.Instance.LoadingData.Find(x => x.Name.ToLower().Contains(name.ToLower())));
    }

    public static void LoadScene(SwipeMenu.Menu MenuRef)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(MenuRef);
    }
    public static void LoadScene(int index)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(index);
    }
    public static void LoadScene(string nameOrPath)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(nameOrPath);
    }
    public static void LoadScene(LoaderUtils loader)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(loader);
    }
    public static void LoadScene(SceneReference Scene)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(Scene);
    }
    public static void LoadScene(SO_SceneData data)
    {
        pupetMonoBehaviour.GetComponent<LoaderUtils>().LoadScene(data.Scene);
    }
}
public class LaadAssets
{
    public SO_SceneData Loaders;
}
