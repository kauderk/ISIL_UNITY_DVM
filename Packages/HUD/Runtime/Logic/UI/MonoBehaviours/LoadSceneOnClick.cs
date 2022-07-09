using UnityEngine;
using UnityEngine.UI;

public class LoadSceneOnClick : Clickable
{
    [SerializeField]
    public SceneID sceneToLoad = SceneID.HUD;
    public override SceneID GetSceneID() => sceneToLoad;
}
