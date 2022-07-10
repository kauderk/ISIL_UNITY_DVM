using UnityEngine;

public class SingletonScriptableObjectReference : MonoBehaviour
{
    public static SingletonScriptableObjectReference instance;
    [SerializeField] private MainManager MainManager;
    [SerializeField] private SO_SceneLoader SO_SceneLoader;
    [SerializeField] private SO_CanvasListener SO_CanvasListener;

    void OnEnable()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
