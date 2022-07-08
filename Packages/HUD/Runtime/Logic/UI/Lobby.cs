using UnityEngine;

public class Lobby : Clickable
{
    [ReadOnly]
    [SerializeField]
    private SceneID sceneToLoad = SceneID.Lobby;
    public override SceneID GetSceneID() => sceneToLoad;
}
