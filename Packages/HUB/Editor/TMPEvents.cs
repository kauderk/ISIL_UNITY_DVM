using UnityEngine;
using UnityEngine.Events;
using TMPro;

[ExecuteAlways]
public class TMPEvents : MonoBehaviour
{
    public OptionalField<float> text = new OptionalField<float>(0);

    // public UnityEvent OnEnable_TMPEvent;

    // private void OnEnable()
    // {
    //     OnEnable_TMPEvent?.Invoke();
    // }

    // public void SetText(ISObject compt)
    // {
    //     var tmp = gameObject.GetComponent<TextMeshProUGUI>();
    //     tmp.text = compt.Name;
    // }
}