using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;

[CreateAssetMenu(fileName = "MenuHelper", menuName = "Loaders/MenuHelper")]
public class MenuSceneHelper : ScriptableObject
{
    void Start()
    {
        // KILL ME!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public async void Quit()
    {
        await SO_SceneLoader.Instance.FadeOut(() => Application.Quit());
    }
    public void DisableButtons(RectTransform obj) => ToggleButtons(obj, false);
    public void EnableButtons(RectTransform obj) => ToggleButtons(obj, true);

    private static void ToggleButtons(RectTransform obj, bool bol)
    {
        foreach (var button in obj.GetComponentsInChildren<Button>())
        {
            button.interactable = bol;
        }
    }
}
