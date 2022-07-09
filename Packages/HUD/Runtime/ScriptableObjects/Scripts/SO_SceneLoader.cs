using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;
using System.Linq;

[System.Serializable]
public class UDisctionary<Tkey, Tval> : SerializableDictionaryBase<Tkey, Tval> { }

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Loaders/SceneLoader")]
public class SO_SceneLoader : SingletonScriptableObject<SO_SceneLoader>
{
    [Tooltip("SO_SceneData are ScriptableObjects")]
    public UDisctionary<SceneID, SO_SceneData> SceneDataDict;

    [Tooltip("A canvas that covers the entire screen")]
    public GameObject FaderCanvasPrefab = null;

    [Tooltip("Fade in/out in specified SceneData Dictionary")]
    public bool SceneDataOnly = true;

    [Tooltip("Fade from black to white")]
    public bool FadeOutOnLoad = true;


    #region Unity State Cycle
    private static GameObject pupetMonoBehaviour;

    private void OnEnable()
    {
        //SceneManager.sceneLoaded += UnitySceneManagerLoaded;
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
    #endregion


    #region Custom Logic
    private void OnLoad()
    {
        pupetMonoBehaviour = new GameObject("Singleton_SceneLoader");
        DontDestroyOnLoad(pupetMonoBehaviour);
        // AppendCanvasTo
        FaderCanvas = Instantiate(FaderCanvasPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        UniTask uniTask = SetupCanvasAndFaderAsync();
    }
    #endregion


    private static GameObject FaderCanvas;
    private static Image fader;
    private static Toggle GlobalVolumeToogle;
    event Action OnFaderLoaded;
    [Tooltip("When the loading begins it won't end until this boolen is set to false")]
    public bool KeepLoading = false;

    public async UniTask SetupCanvasAndFaderAsync()
    {
        // get the firs children
        fader = FaderCanvas.transform.GetChild(0).GetComponent<Image>();
        var buffer = 20;
        fader.rectTransform.sizeDelta = new Vector2(Screen.width + buffer, Screen.height + buffer);
        // show
        FaderCanvas.SetActive(true);
        if (SceneDataOnly)
        {
            var sceneName = SceneManager.GetActiveScene().path;
            var exist = SceneDataDict.Values.Any(so => so.Scene.ScenePath == sceneName);
            if (!exist)
                return;
        }
        ToogleFader(FadeOutOnLoad);
        if (FadeOutOnLoad)
        {
            ToogleFader(true);
            await LerpFader(1, 0, new Fade());
            ToogleFader(false);
        }
    }
    public void ToogleFader(bool b) => fader.gameObject.SetActive(b);

    public void LoadSceneAwaiter(SceneID id) => LoadScene(id, null).GetAwaiter();
    public async UniTask LoadScene<T>(T sceneID, Fade fade = null)
    {
        fade = fade ?? new Fade(); // come ON!
        var sceneName = UnitySceneUtils.DoesSceneExist(sceneID);
        // YIKES!
        if (sceneName != null)
        {
            await SO_SceneLoader.Instance.FadeScene(sceneName, fade);
            return;
        }
        Debug.Log("Scene ID must be string or int");
    }

    public async UniTask FadeScene<T>(T sceneID, Fade fade)
    {
        ToogleFader(true);

        await LerpFader(0, 1, fade);

        await UniTask.Delay(TimeSpan.FromSeconds(fade.waitTime), ignoreTimeScale: false);

        var ao = typeof(T) == typeof(int)
            ? SceneManager.LoadSceneAsync((int)(object)sceneID)
            : SceneManager.LoadSceneAsync((string)(object)sceneID); // https://stackoverflow.com/questions/4092393/value-of-type-t-cannot-be-converted-to#:~:text=string%20newT2%20%3D%20(string)(object)t%3B

        while (!ao.isDone || KeepLoading)
            await UniTask.Yield();

        await LerpFader(1, 0, fade);

        ToogleFader(false);

        OnFaderLoaded?.Invoke();

    }
    async UniTask LerpFader(int from, int to, Fade fade)
    {
        for (float t = 0; t < 1; t += Time.deltaTime / fade.duration)
        {
            fader.color = new Color(0, 0, 0, Mathf.Lerp(from, to, t));
            await UniTask.Yield();
        }
    }

}
