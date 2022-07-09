using Cysharp.Threading.Tasks;
using UnityEngine;
using Photon.Pun;

public class Launcher_ : MonoBehaviourPunCallbacks
{
    [SerializeField] private PhotonView playerPrefab;
    [SerializeField] private PhotonView cameraPrefab;

    private void Awake()
    {
    }

    void Start()
    {
        if (SceneController.Instance)
            SceneController.Instance.isForcedToLoad = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado con exito al servidor");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override async void OnJoinedRoom()
    {
        if (SceneController.Instance)
            SceneController.Instance.isForcedToLoad = false;
        Debug.Log("Ingreso exitoso a sala");

        var player = await YieldUntilPlayer();
    }

    async UniTask<GameObject> YieldUntilPlayer()
    {
        // WTF!

        var player = PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, .5f, 0), Quaternion.identity, 1, null);
        Debug.Log(player);
        while (!player.transform || !player)
        {
            Debug.Log("...jugador");
            await UniTask.Yield();
        }

        if (!cameraPrefab)
        {
            Debug.Log("FAIL camera");
            return null;
        }
        var camera = PhotonNetwork.Instantiate(cameraPrefab.name, new Vector3(0, 0, 0), Quaternion.identity, 1, null);
        camera.gameObject.SetActive(false);
        //camera.GetComponent<MenteBacata.ScivoloCharacterControllerDemo.OrbitingCamera>().enabled = false;
        while (!camera.transform || !camera)
        {
            Debug.Log("...camara");
            await UniTask.Yield();
        }
        camera.gameObject.SetActive(true);
        var orbit = Camera.main;//camera.GetComponent<MenteBacata.ScivoloCharacterControllerDemo.OrbitingCamera>();
        while (!orbit)
        {
            Debug.Log("...orbit");
            await UniTask.Yield();
        }
        // Debug.Log(orbit.target);
        // Debug.Log(player.transform);
        // orbit.target = player.transform;
        //camera.GetComponent<MenteBacata.ScivoloCharacterControllerDemo.OrbitingCamera>().enabled = false;
        return player;
    }
}
