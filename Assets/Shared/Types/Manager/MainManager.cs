using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MainManager", menuName = "Singleton/MainManager")]
public class MainManager : SingletonScriptableObject<MainManager>
{
    public static Action OnGameInitialized;
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void FirstInitialize()
    {
        if (Application.isPlaying)
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
            OnGameInitialized?.Invoke();
        }
    }
    public static readonly List<Action> OnGameInitializedActions = new List<Action>();
    public static void RegisterOnGameInitialized(Action action)
    {
        OnGameInitializedActions.Add(action);
        OnGameInitialized += action;
    }
    private void OnDisable()
    {
        CleanOnGameInitializedActions();
        //GlobalEvents.ClanIDToEventMap();
    }

    private static void CleanOnGameInitializedActions()
    {
        Array.ForEach(OnGameInitializedActions.ToArray(), (action) =>
        {
            if (action == null)
                return;

            foreach (var cb in action.GetInvocationList())
                action -= (Action)cb;
            OnGameInitializedActions.Remove(action);
        });
    }
}