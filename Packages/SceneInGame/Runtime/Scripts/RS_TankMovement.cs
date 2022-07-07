using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_TankMovement : MonoBehaviour
{
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;
    public ParticleSystem dustTrail;
    public ParticleSystem dustTrailBack;
    public Animator tankMoveAnime;
    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * Time.deltaTime * rotationSpeed, 0);
        transform.Translate(0, 0, Input.GetAxis("Vertical") * Time.deltaTime * movementSpeed);

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            dustTrail.Play();
            tankMoveAnime.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            dustTrail.Stop();
            tankMoveAnime.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            dustTrailBack.Play();
            tankMoveAnime.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.DownArrow))
        {
            dustTrailBack.Stop();
            tankMoveAnime.SetBool("IsMoving", false);
        }
    }
}
