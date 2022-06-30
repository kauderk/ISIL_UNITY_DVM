using UnityEngine;

namespace Networking
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance;
        private void Awake()
        {
            Instance = this;
        }

        [SerializeField] Menu[] menus;

        public void OpenMenu(string name)
        {
            foreach (var m in menus)
                if (m.Name == name)
                    OpenMenu(m);
                else if (m.opened)
                    CloseMenu(m);
        }
        public void OpenMenu(Menu menu)
        {
            foreach (var m in menus)
                if (m.opened)
                    CloseMenu(m);
            menu.Open();
        }
        public void CloseMenu(Menu menu)
        {
            menu.Close();
        }
    }
}