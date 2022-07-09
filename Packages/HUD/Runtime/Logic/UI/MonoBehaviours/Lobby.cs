using UnityEngine;

public class Lobby : Clickable
{
    [ReadOnly]
    [SerializeField]
    private SceneID sceneToLoad = SceneID.HUD;
    public override SceneID GetSceneID() => sceneToLoad;
}
