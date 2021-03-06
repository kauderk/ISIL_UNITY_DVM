using UnityEngine;

namespace Visual
{
    public interface ISkin
    {
        void ApplyColor(Color color);
        void Flicker();
        //void SetSkin(string skinName);
    }
    public static class VisualUtils
    {
        public static void ApplyDefaultMaterial(this MeshRenderer mesh, Color color)
        {
            var meshRenderer = mesh;
            var temp = new Material(Shader.Find("Standard")) { color = color };
            meshRenderer.material = temp;
        }
    }
}
