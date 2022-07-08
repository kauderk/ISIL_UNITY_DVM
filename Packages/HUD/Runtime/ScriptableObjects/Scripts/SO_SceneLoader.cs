using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

[System.Serializable]
public class UDisctionary<Tkey, Tval> : SerializableDictionaryBase<Tkey, Tval> { }

[CreateAssetMenu(fileName = "SceneLoader", menuName = "Loaders/SceneLoader")]
public class SO_SceneLoader : SingletonScriptableObject<SO_SceneLoader>
{
    [Tooltip("Must to respect Enum.SceneID order")]
    //public List<SO_SceneData> LoadingData = new List<SO_SceneData>();
    //public readonly Dictionary<SceneID, SO_SceneData> SceneDataDict = new Dictionary<SceneID, SO_SceneData>();
    public UDisctionary<SceneID, SO_SceneData> SceneDataDict;
    //public UDictionarySceneData SceneDataDict;
    //public SerializableDictionary<SceneID, SO_SceneData> SceneDataDict = new SerializableDictionary<SceneID, SO_SceneData>();
    public GameObject FaderCanvasPrefab = null;
    public bool FadeOutOnLoad = true;
    //public SceneUtils SceneUtils_ = new SceneUtils();


    #region Unity State Cycle
    private static GameObject pupetMonoBehaviour;

    private void OnEnable()
    {
        // loop trough the SceneID enum
        // IList list = Enum.GetValues(typeof(SceneID));
        // for (int i = 0; i < list.Count; i++)
        // {
        //     SceneID sceneID = (SceneID)list[i];
        //     SO_SceneData sceneData = LoadingData[i];
        //     SceneDataDict[sceneID] = sceneData;
        // }
        //LoadingData.ForEach(x => SceneDataDict.Add(x.SceneID, x));

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
    public bool isForcedToLoad = false;

    public async UniTask SetupCanvasAndFaderAsync()
    {
        // get the firs children
        fader = FaderCanvas.transform.GetChild(0).GetComponent<Image>();
        var buffer = 20;
        fader.rectTransform.sizeDelta = new Vector2(Screen.width + buffer, Screen.height + buffer);
        // show
        FaderCanvas.SetActive(true);
        ToogleFader(FadeOutOnLoad);
        if (FadeOutOnLoad)
        {
            ToogleFader(true);
            await LerpFader(1, 0, new Fade());
            ToogleFader(false);
        }
    }
    public void ToogleFader(bool b) => fader.gameObject.SetActive(b);


    public async UniTask LoadScene<T>(T sceneID, Fade fade = null)
    {
        fade = fade ?? new Fade(); // come ON!
        // YIKES!
        if (UnitySceneUtils.DoesSceneExist(sceneID))
        {
            await SO_SceneLoader.Instance.FadeScene(sceneID, fade);
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

        while (!ao.isDone || isForcedToLoad)
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
public class Fade
{
    public float duration = 5;
    public float waitTime = 2;
}

public enum SceneID
{
    Lobby,
    HUD,
    Enviroment,
    Gameplay
}

[System.Serializable]
public class SceneData
{
    public string Name;
    public SO_SceneData SP_SceneAsset;
}
