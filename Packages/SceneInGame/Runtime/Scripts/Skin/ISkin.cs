using UnityEngine;

namespace Visual
{
    public interface ISkin
    {
        void ApplyMaterial(Color color);
        //void SetSkin(string skinName);
    }
    public static class VisualUtils
    {
        public static void ApplyMaterial(this MeshRenderer mesh, Color color)
        {
            var meshRenderer = mesh;
            var temp = new Material(Shader.Find("Standard")) { color = color };
            meshRenderer.material = temp;
        }
    }
}
