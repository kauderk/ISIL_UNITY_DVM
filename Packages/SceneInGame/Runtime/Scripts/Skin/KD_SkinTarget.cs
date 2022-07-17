using System;
using UnityEngine;
using EventBusSystem;
using Visual;

public interface IVisualChange : IGlobalSubscriber
{
    void OnBusRaise(Color color);
}

[RequireComponent(typeof(MeshRenderer))]
public class KD_SkinTarget : MonoBehaviour, ISkin, IVisualChange
{
    private void OnEnable() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);
    public void OnBusRaise(Color c) => ApplyColor(c);

    [ReadOnly]
    public MeshRenderer mesh;

    public void Awake() => mesh = GetComponent<MeshRenderer>();

    public void ApplyColor(Color color)
    {
        if (!mesh)
            Awake();
        mesh.ApplyColor(color);
    }
}
