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
    public GameObject FaderCanvasPrefab = null;
    //public SceneUtils SceneUtils_ = new SceneUtils();


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
        // AppendCanvasTo
        FaderCanvas = Instantiate(FaderCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        FaderCanvas.SetActive(true);
        SetupFader();
        //SceneUtils.ToogleFader(true);
    }
    public static void RequestSceneLoad(string name)
    {
        LoadScene(SO_SceneLoader.Instance.LoadingData.Find(x => x.Name.ToLower().Contains(name.ToLower())));
    }
    public void UnitySceneManagerLoaded(Scene scene, LoadSceneMode mode)
    {
        //AudioListener.pause = GlobalVolumeToogle.isOn;
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



    static GameObject FaderCanvas;
    private Image fader;
    private Toggle GlobalVolumeToogle;
    event Action OnFaderLoaded;
    public bool isForcedToLoad = false;

    public void SetupFader()
    {
        // get the firs children
        fader = FaderCanvas.transform.GetChild(0).GetComponent<Image>();
        var buffer = 20;
        fader.gameObject.SetActive(true);
        fader.rectTransform.sizeDelta = new Vector2(Screen.width + buffer, Screen.height + buffer);
    }
    public void ToogleFader(bool b) => fader.enabled = b;

    public async UniTask LoadScene<T>(T sceneID, float duration = 1, float waitTime = 2)
    {
        // YIKES!
        if (UnitySceneUtils.DoesSceneExist(sceneID))
        {
            await FadeScene(sceneID, duration, waitTime);
            return;
        }
        Debug.Log("Scene ID must be string or int");
    }
    async UniTask FadeScene<T>(T sceneID, float duration, float waitTime)
    {
        ToogleFader(true);

        await LerpFader(0, 1);

        await UniTask.Delay(TimeSpan.FromSeconds(waitTime), ignoreTimeScale: false);

        var ao = typeof(T) == typeof(int)
            ? SceneManager.LoadSceneAsync((int)(object)sceneID)
            : SceneManager.LoadSceneAsync((string)(object)sceneID); // https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to#:~:text=string%20newT2%20%3D%20(string)(object)t%3B

        while (!ao.isDone || isForcedToLoad)
            await UniTask.Yield();

        await LerpFader(1, 0);

        ToogleFader(false);

        OnFaderLoaded?.Invoke();

        async UniTask LerpFader(int from, int to)
        {
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                fader.color = new Color(0, 0, 0, Mathf.Lerp(from, to, t));
                await UniTask.Yield();
            }
        }
    }
}
