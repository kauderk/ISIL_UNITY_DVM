using System;
using UnityEngine;
using Visual;

[RequireComponent(typeof(MeshRenderer))]
public class KD_SkinTarget : MonoBehaviour, ISkin
{
    [ReadOnly]
    public MeshRenderer mesh;

    public void Awake() => mesh = GetComponent<MeshRenderer>();

    private void Start()
    {
        //GlobalEvents.Subscribe<Color>(IDs.pickupMagazine, ApplyColor);
    }
    public void ApplyColor(Color color)
    {
        if (!mesh)
            Awake();
        mesh.ApplyColor(color);
    }
}
