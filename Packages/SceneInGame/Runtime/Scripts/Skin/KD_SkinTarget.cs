using System;
using UnityEngine;
using EventBusSystem;
using Visual;

public interface ITestBus : IGlobalSubscriber
{
    void HandleSubscription();
}

[RequireComponent(typeof(MeshRenderer))]
public class KD_SkinTarget : MonoBehaviour, ISkin, ITestBus
{
    private void OnEnable() => EventBus.Subscribe(this);
    private void OnDisable() => EventBus.Unsubscribe(this);

    public void HandleSubscription() => Debug.Log("Quick save");

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
