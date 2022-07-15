using UnityEngine;
using UnityEngine.Events;
using Photon.Realtime;

namespace Photon.Pun
{
    public class LauncherSetup : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private bool createRoomOnPhotonJoinedLobby;
        public void SetRoomNameInputField(string roomName) => SO_PhotonLauncher.Instance.roomName = roomName;

        #region Unity Events
        public UnityEvent OnPhotonJoinedLobby;
        public UnityEvent<string> OnPhotonCreatedRoom;
        public UnityEvent<string> OnPhotonJoinedRoom;
        public UnityEvent OnPhotonLeftRoom;
        public UnityEvent<string> OnPhotonFailedToConnect;
        #endregion

        #region Photon SetUp
        void Start()
        {
            Log("Conecting Photon PUN...");
            if (!PhotonNetwork.IsConnected)
                PhotonNetwork.ConnectUsingSettings();
            else
                OnConnectedToMaster();
        }
        public override void OnConnectedToMaster()
        {
            Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            Log("Joined lobby");
            OnPhotonJoinedLobby?.Invoke();
            if (createRoomOnPhotonJoinedLobby)
                CreateRoom();
        }
        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(SO_PhotonLauncher.Instance.RoomName))
            {
                Debug.LogWarning("Room name is empty");
                return;
            }
            PhotonNetwork.JoinOrCreateRoom(SO_PhotonLauncher.Instance.RoomName, new RoomOptions { MaxPlayers = 3 }, TypedLobby.Default);
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Log("Room Creation Failed: " + message);
            OnPhotonFailedToConnect?.Invoke(message);
        }
        public void Log(string msg)
        {
            if (SO_PhotonLauncher.Instance.ShowDebug)
                Debug.Log(msg);
        }
        #endregion
    }
}
