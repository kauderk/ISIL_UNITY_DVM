using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_TankShoot : MonoBehaviour
{
    public ParticleSystem fireShot;
    public Animator camAnim;
    public Animator TankShotAnim;

    private float timer;
    private float timer02;

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKey(KeyCode.C) && timer > 1)
        {
            fireShot.Play();
            CamShake();
            TankShotAnim.SetTrigger("TankShoot");
            timer = 0;
        }

        if (Input.GetKeyUp(KeyCode.C))
            fireShot.Stop();

    }
    public void CamShake()
    {
        int rand = Random.Range(0, 3);
        if (rand == 0)
        {
            camAnim.SetTrigger("Shake");
        }
        else if (rand == 1)
        {
            camAnim.SetTrigger("Shake02");
        }
        else if (rand == 2)
        {
            camAnim.SetTrigger("Shake03");
        }
    }
}
