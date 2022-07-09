using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SwipeMenu;
using Cysharp.Threading.Tasks;

[CreateAssetMenu(fileName = "CanvasListener", menuName = "UI/CanvasListener")]
public class SO_CanvasListener : SingletonScriptableObject<SO_CanvasListener>
{
    public Action OnCanvasCreated;
    private void OnEnable() => MainManager.OnGameInitialized += OnLoad;
    private void OnDisable() => MainManager.OnGameInitialized -= OnLoad;
    private void OnLoad()
    {

    }
    public void LoadCurrentMenuItem() => LoadCurrentMenuItemAsync().GetAwaiter();
    public async UniTask LoadCurrentMenuItemAsync()
    {
        // HARD CODED FOR NOW
        var index = Menu.instance.getCurrentItem();
        var sceneData = Menu.instance.transform.GetChild(index).GetComponent<MenuItem>().SceneData;
        await SO_SceneLoader.Instance.LoadScene(sceneData.Scene);
    }
}
