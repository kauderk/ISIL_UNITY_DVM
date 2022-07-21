using UnityEngine;
using Photon.Pun;

public class RS_TankAnimations : MonoBehaviourPunBase
{
    [SerializeField] private ParticleSystem dustPS01;
    [SerializeField] private ParticleSystem dustPS02;
    [SerializeField] private Animator tankAnime;

    protected override void MyUpdate()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            dustPS01.Play();
            tankAnime.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            dustPS01.Stop();
            tankAnime.SetBool("IsMoving", false);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            dustPS02.Play();
            tankAnime.SetBool("IsMoving", true);
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            dustPS02.Stop();
            tankAnime.SetBool("IsMoving", false);
        }
    }
}
