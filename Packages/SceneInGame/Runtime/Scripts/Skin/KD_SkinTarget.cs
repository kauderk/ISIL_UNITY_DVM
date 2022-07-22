using System;
using UnityEngine;
using EventBusSystem;
using Visual;
using System.Collections;

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
        mesh.ApplyDefaultMaterial(color);
    }

    public void Flicker()
    {
        StartCoroutine(FlickerCrr());
    }
    IEnumerator FlickerCrr()
    {
        var renderer = GetComponent<MeshRenderer>();
        var startColor = renderer.material.color;
        var endColor = Color.red;
        var time = 0f;
        // Flicker for 1 second
        while (time < .15f)
        {
            if (gameObject == null)
                yield break;
            time += Time.deltaTime;
            // ping-pong between start and end color
            renderer.material.color = startColor == renderer.material.color ? endColor : startColor;
            //renderer.material.color = Color.Lerp(startColor, endColor, time / 1f);
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(0.05f);
        renderer.material.color = startColor;
    }
}
