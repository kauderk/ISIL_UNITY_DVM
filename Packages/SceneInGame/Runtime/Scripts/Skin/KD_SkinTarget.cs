using UnityEngine;
using Visual;

[RequireComponent(typeof(MeshRenderer))]
public class KD_SkinTarget : MonoBehaviour, ISkin
{
    [ReadOnly]
    public MeshRenderer mesh;

    public void Awake() => mesh = GetComponent<MeshRenderer>();
    public void ApplyMaterial(Color color)
    {
        if (!mesh)
            Awake();
        mesh.ApplyMaterial(color);
    }
}
