using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_TankShowShoot : MonoBehaviour
{
    public ParticleSystem fireShot;
    public Animator camAnim;
    public Animator TankShotAnim;

    private float timer;

    readonly List<string> shakes = new List<string>() { "Shake01", "Shake02", "Shake03" };

    void Update()
    {
        timer += Time.deltaTime;
        if (InputShoot() && timer > 1)
        {
            fireShot.Play(); // TODO: listen to the CanFire() event
            CamShake();
            TankShotAnim.SetTrigger("TankShoot");
            timer = 0;
        }
        if (InputShootRelese())
        {
            fireShot.Stop();
        }
    }
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
    bool InputShoot() => Input.GetMouseButton(0) || Input.GetMouseButtonDown(0);
    bool InputShootRelese() => Input.GetMouseButtonUp(0);
}
