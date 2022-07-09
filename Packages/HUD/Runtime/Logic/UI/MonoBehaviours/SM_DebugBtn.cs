using UnityEngine;
using UnityEngine.UI;
using SwipeMenu;

[RequireComponent(typeof(Text))]
public class SM_DebugBtn : MonoBehaviour
{
    private Text text;

    private void Awake() => text = GetComponent<Text>();

    public void UpdateColour(MenuItem item)
    {
        text.color = item.gameObject.GetComponent<Renderer>().material.color;
    }
}
