using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System;
using Photon.Pun;

[CreateAssetMenu(fileName = "MenuHelper", menuName = "Loaders/MenuHelper")]
public class MenuSceneHelper : ScriptableObject
{
    public string UserRoomName = "ISIL_DVM";
    public string UserNickname = "Player";
    // set UserRoomName
    public void SetUserRoomName(string roomName)
    {
        //PhotonNetwork.CurrentRoom.Name = roomName;
        UserRoomName = roomName;
    }
    public void SetUserNickname(string nickname)
    {
        PhotonNetwork.NickName = UserNickname = nickname;
    }

    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Confined;
        ShowCursor();
    }

    public void ShowCursor(bool b = true) => Cursor.visible = b;

    public async void Quit()
    {
        await SO_SceneLoader.Instance.FadeOut(() => Application.Quit());
    }
    public async void FadeOut()
    {
        await SO_SceneLoader.Instance.FadeOut(() => { });
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
