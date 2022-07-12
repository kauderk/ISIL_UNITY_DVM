using UnityEngine;
using Photon.Pun;

public class RS_TankMovement : MonoBehaviourPunCallbacks
{
    public SO_PlayerSettings settings;

    private void Awake() => settings.Init(gameObject);

    void Update()
    {
        //if (photonView.IsMine)
        //{
        Move();
        Show();
        //}
    }

    private void Move()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * settings.rotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * settings.movementSpeed);

        void isMoving(bool bol) => settings.animator.SetBool("isMoving", bol);

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
            settings.dustTrail.Play();

        if (InputForwardsStoped())
            settings.dustTrail.Stop();

        if (InputBackwards())
            settings.dustTrailBack.Play();

        if (InputBackwardsStoped())
            settings.dustTrailBack.Stop();
    }

    #region Inputs
    bool InputForwards() => Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow);
    bool InputForwardsStoped() => Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow);
    bool InputBackwards() => Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow);
    bool InputBackwardsStoped() => Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow);
    #endregion
}
