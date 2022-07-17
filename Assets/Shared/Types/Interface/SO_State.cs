using UnityEngine;

[System.Serializable]
public class SO_State<T>
{
    [field: SerializeField]
    public T Editor;

    [field: SerializeField]
    public bool ResetOnLoad { get; private set; } = true;

    [field: SerializeField, ReadOnly]
    public T Runtime;
}