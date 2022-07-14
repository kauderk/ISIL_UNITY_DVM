using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_RandomTankColor : MonoBehaviour
{
    void ApplyMaterial(MeshRenderer mesh, Color color)
    {
        var mat = new Material(Shader.Find("Standard"));
        mat.SetColor("_Color", color);
        mesh.material = mat;
    }
}
