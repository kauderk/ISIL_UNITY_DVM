using UnityEngine;
using UnityEngine.UI;

public class Clickable : MonoBehaviour, Iclick
{
    public void OnClick() => SO_SceneLoader.RequestSceneLoad("lobby");
    private void Awake() => GetComponent<Button>().onClick.AddListener(OnClick);

}
