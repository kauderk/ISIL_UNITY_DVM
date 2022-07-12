using UnityEngine;

public class Lobby : Clickable
{
    [SerializeField, ReadOnly]
    private SceneID sceneToLoad = SceneID.HUB;
    public override SceneID GetSceneID() => sceneToLoad;
}
