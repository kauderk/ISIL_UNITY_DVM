using UnityEngine;
using Photon.Pun;

public class RS_TankShowMovement : MonoBehaviourPunCallbacks
{
    public KD_TankEditorSettings EditorSettings;

    void isMoving(bool bol) => EditorSettings.animator.SetBool("isMoving", bol);
    void forwardTrail(bool bol)
    {
        if (bol) EditorSettings.dustTrail.Play();
        else EditorSettings.dustTrail.Stop();
    }
    void backwardTrail(bool bol)
    {
        if (bol) EditorSettings.dustTrailBack.Play();
        else EditorSettings.dustTrailBack.Stop();
    }

    private void Update()
    {
        if (InputForwards())
        {
            isMoving(true);
            backwardTrail(true);
        }
        if (InputForwardsStoped())
        {
            isMoving(false);
            backwardTrail(false);
        }
        if (InputBackwards())
        {
            isMoving(true);
            backwardTrail(true);
        }
        if (InputBackwardsStoped())
        {
            isMoving(false);
            backwardTrail(false);
        }
    }

    #region Inputs
    bool InputForwards() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    bool InputForwardsStoped() => Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    bool InputBackwards() => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    bool InputBackwardsStoped() => Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
    #endregion
}
