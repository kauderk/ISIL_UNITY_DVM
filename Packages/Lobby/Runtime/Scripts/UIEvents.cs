using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class UIEvents : MonoBehaviour
{
    public UnityEvent OnEnableEvent;
    public void OnEnable()
    {
        OnEnableEvent?.Invoke();
    }


    public void SetText(string text)
    {
        gameObject.GetComponent<TMP_Text>().text = text;
    }
}
