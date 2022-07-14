using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Visual;

public class KD_SkinMono : MonoBehaviour
{
    public bool ChangeOnStart = true;
    public bool random;
    public Color Color;

    public void Awake()
    {
        if (ChangeOnStart)
            NotifySiblings();
    }
    public void NotifySiblings()
    {
        transform.NotifySiblings<ISkin>(I => I.ApplyMaterial(GetColor()));
    }
    Color GetColor()
    {
        return !random ? Color : Random.ColorHSV(0f, .5f, 0f, 0.5f);
    }
}
