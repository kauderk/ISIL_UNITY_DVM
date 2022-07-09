using UnityEngine;

public class Lobby : Clickable
{
    [ReadOnly]
    [SerializeField]
    private SceneID sceneToLoad = SceneID.HUB;
    public override SceneID GetSceneID() => sceneToLoad;
}
