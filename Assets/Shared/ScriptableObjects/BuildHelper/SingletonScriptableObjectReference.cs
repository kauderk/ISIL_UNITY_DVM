using UnityEngine;
using Photon.Pun;
using Store;

public class SingletonScriptableObjectReference : MonoBehaviour
{
    public static SingletonScriptableObjectReference instance;
    [SerializeField] private MainManager MainManager;
    [SerializeField] private SO_SceneLoader SO_SceneLoader;
    [SerializeField] private SO_CanvasListener SO_CanvasListener;
    [SerializeField] private SO_RedirectSignalManager SO_RedirectSignalManager;
    [SerializeField] private SO_DependencyManager SO_DependencyManager;
    [SerializeField] private SO_CanvasListener SO_CanvasListener2;
    [SerializeField] private SO_PhotonLauncher SO_PhotonLauncher;
    [SerializeField] private SO_Artillery SO_Artillery;
    [SerializeField] private AudioManager SO_AudioManager;


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
