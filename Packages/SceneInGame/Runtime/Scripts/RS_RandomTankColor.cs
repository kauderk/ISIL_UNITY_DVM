using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RS_RandomTankColor : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] meshColor;

    void Start()
    {
        Color newColor = Random.ColorHSV(0f, .5f, 0f, 0.5f);
        ApplyMaterial(newColor, 0);
    }
    void ApplyMaterial(Color color, int targetMaterialIndex)
    {
        Material generatedMaterial = new Material(Shader.Find("Standard"));
        generatedMaterial.SetColor("_Color", color);
        for (int i = 0; i < meshColor.Length; i++)
        {
            meshColor[i].material = generatedMaterial;
        }
    }
}
