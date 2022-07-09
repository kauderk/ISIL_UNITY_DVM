using UnityEngine;
using UnityEngine.UI;

public class LoadSceneOnClick : Clickable
{
    [SerializeField]
    public SceneID sceneToLoad = SceneID.HUB;
    public override SceneID GetSceneID() => sceneToLoad;
}
