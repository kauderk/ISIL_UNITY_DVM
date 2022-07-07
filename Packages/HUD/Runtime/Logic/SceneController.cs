using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public Image fader;
    public Toggle GlobalVolumeToogle;
    public static SceneController Instance;

    event Action OnFaderLoaded;
    public bool isForcedToLoad = false;

    public void ToogleFader(bool b) => fader.gameObject.SetActive(b);

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //AudioListener.pause = GlobalVolumeToogle.isOn;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        var buffer = 20;
        fader.rectTransform.sizeDelta = new Vector2(Screen.width + buffer, Screen.height + buffer);
        ToogleFader(false);
        DontDestroyOnLoad(gameObject);
    }

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
