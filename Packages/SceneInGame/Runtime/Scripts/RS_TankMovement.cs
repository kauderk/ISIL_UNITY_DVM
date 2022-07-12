using UnityEngine;
using Photon.Pun;

public class RS_TankMovement : MonoBehaviourPunCallbacks
{
    public KD_TankEditorSettings EditorSettings;

    void Update()
    {
        Move();
        Show();
    }

    private void Move()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * EditorSettings.SO.rotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * EditorSettings.SO.movementSpeed);

        void isMoving(bool bol) => EditorSettings.animator.SetBool("isMoving", bol);

        if (InputForwards())
            isMoving(true);

        if (InputForwardsStoped())
            isMoving(false);

        if (InputBackwards())
            isMoving(true);

        if (InputBackwardsStoped())
            isMoving(false);
    }
    private void Show()
    {
        if (InputForwards())
            EditorSettings.dustTrail.Play();

        if (InputForwardsStoped())
            EditorSettings.dustTrail.Stop();

        if (InputBackwards())
            EditorSettings.dustTrailBack.Play();

        if (InputBackwardsStoped())
            EditorSettings.dustTrailBack.Stop();
    }

    #region Inputs
    bool InputForwards() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    bool InputForwardsStoped() => Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    bool InputBackwards() => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    bool InputBackwardsStoped() => Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
    #endregion
}
