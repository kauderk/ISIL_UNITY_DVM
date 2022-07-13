using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signals
{
    public Action<GameObject> PickUp;
    public static Dictionary<string, GameObject> subscribersDict = new Dictionary<string, GameObject>();
}

[CreateAssetMenu(fileName = "RedirectSignalManager", menuName = "Managers/RedirectSignalManager")]
public class SO_RedirectSignalManager : SingletonScriptableObject<SO_RedirectSignalManager>
{
    public static Signals Signal;

    #region state managment
    void OnEnable() => MainManager.OnGameInitialized += OnLoad;
    void OnDisable() => MainManager.OnGameInitialized -= OnLoad;

    public void OnLoad()
    {
        foreach (var subscriber in Signals.subscribersDict.Values)
        {
            if (Application.isPlaying)
                Destroy(subscriber);
            else
                DestroyImmediate(subscriber);
        }
    }
    #endregion


    public static void UseUpwardSignal<T>(GameObject subscriber) where T : KD_ISignalListener
    {
        NewMethod<T>(subscriber);
    }

    private static void NewMethod<T>(GameObject subscriber) where T : KD_ISignalListener
    {
        var parent = subscriber.transform.parent.gameObject;
        while (parent)
        {
            if (parent.gameObject.TryGetComponent<T>(out var listner))
            {
                listner.OnSignal();
                return;
            }
            parent = parent.transform.parent.gameObject;
        }
    }

    public static void UseLateralSignal<T>(GameObject subscriber) where T : KD_ISignalListener
    {
        NewMethod<T>(subscriber);
    }
}
