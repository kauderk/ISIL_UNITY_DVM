using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour //, Iclick
{
    public virtual SceneID GetSceneID() => SceneID.Lobby;

    private void Awake() =>
        GetComponent<Button>().onClick.AddListener(async ()
            => await SO_SceneLoader.Instance.LoadScene(GetSceneID())
        );
}