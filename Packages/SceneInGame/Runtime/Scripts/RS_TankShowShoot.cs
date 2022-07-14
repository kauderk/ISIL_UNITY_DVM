using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapon;

public class RS_TankShowShoot : MonoBehaviour, IFireEvent, ICameraEvents
{
    public ParticleSystem fireShot;
    [SerializeField] Animator camAnim;
    public Animator TankShotAnim;

    [SerializeField, BeginReadOnlyGroup]
    List<string> shakes = new List<string>() { "Shake01", "Shake02", "Shake03" };

    public void OnFire()
    {
        fireShot.Play();
        CamShake();
        TankShotAnim.SetTrigger("TankShoot");
    }

    public void OnStopFire() => fireShot.Stop();

    public void CamShake()
    {
        // return; // the animator is broken FIXME:
        // if (!camAnim)
        // {
        //     Debug.LogWarning("RS_TankShoot: No Camera Animator Found");
        // }
        // int rand = Random.Range(0, shakes.Count); // rando!
        // camAnim.SetTrigger(shakes[rand]);
    }

    public void OnCameraAnimatorChange(Animator camera)
    {
        camAnim = camera;
    }
    public void OnCameraTriggersChange(List<string> shakes)
    {
        this.shakes.Clear();
        this.shakes.AddRange(shakes);
    }
}
