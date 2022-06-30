using UnityEngine;

namespace Networking
{
    public class Menu : MonoBehaviour
    {
        public string Name;
        [HideInInspector] public bool opened;

        private void Awake() => opened = true;

        public void Open()
        {
            opened = true;
            gameObject.SetActive(true);
        }
        public void Close()
        {
            opened = false;
            gameObject.SetActive(false);
        }
    }
}
