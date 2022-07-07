using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Loaders/SceneLoader")]
public class SO_SceneLoader : SingletonScriptableObject<SO_SceneLoader>
{
    public List<SO_SceneData> LoadingData = new List<SO_SceneData>();
    public SceneUtils SceneUtils = new SceneUtils();


    #region Unity State Cycle
    private static GameObject pupetMonoBehaviour;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += UnitySceneManagerLoaded;
        MainManager.OnGameInitialized += OnLoad;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= UnitySceneManagerLoaded;
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
    #endregion


    #region Custom Logic
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
    public void UnitySceneManagerLoaded(){

    }

    #endregion


    #region Function Wrappers
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
    #endregion

}

[System.Serializable]
public class SceneUtils
{
    public GameObject FaderCanvas;
    public Image fader;
    public Toggle GlobalVolumeToogle;
    public static SceneUtils Instance;
    event Action OnFaderLoaded;
    public bool isForcedToLoad = false;

    public SceneUtils()
    {
        fader = FaderCanvas.GetComponentInChildren<Image>();

        // Instance = this;
        // OnFaderLoaded += () =>
        // {
        //     FaderCanvas.SetActive(false);
        //     fader.gameObject.SetActive(false);
        //     GlobalVolumeToogle.gameObject.SetActive(false);
        // };
    }
    public void ToogleFader(bool b) => fader.gameObject.SetActive(b);

}
