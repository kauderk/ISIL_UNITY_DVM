using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class RS_TankShowShoot : MonoBehaviour, IFireEvent
{
    public ParticleSystem fireShot;
    public Animator camAnim;
    public Animator TankShotAnim;

    readonly List<string> shakes = new List<string>() { "Shake01", "Shake02", "Shake03" };

    public void OnFire()
    {
        fireShot.Play();
        CamShake();
        TankShotAnim.SetTrigger("TankShoot");
    }

    public void OnStopFire() => fireShot.Stop();

    public void CamShake()
    {
        if (!camAnim)
        {
            Debug.LogWarning("RS_TankShoot: No Camera Animator Found");
            return;
        }
        int rand = Random.Range(0, 3);
        var shake = shakes[rand];
        camAnim.SetTrigger(shake);
    }
}
