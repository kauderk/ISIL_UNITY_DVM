using UnityEngine;
using UnityEngine.Events;
using Photon.Realtime;
using System;

namespace Photon.Pun
{
    public sealed class LauncherNW : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private bool createRoomOnPhotonJoinedLobby;
        string roomName = "ISIL_DVM_Room";
        public void SetRoomNameInputField(string roomName) => this.roomName = roomName;

        public bool ShowDebug = true;

        #region Unity Events
        public UnityEvent OnPhotonJoinedLobby;
        public UnityEvent<string> OnPhotonCreatedRoom;
        public UnityEvent<string> OnPhotonJoinedRoom;
        public UnityEvent OnPhotonLeftRoom;
        public UnityEvent<string> OnPhotonFailedToConnect;
        #endregion

        #region Photon SetUp
        public static Func<bool> IsMine { get; private set; } // kinda spaghetti

        void Awake()
        {
            IsMine = () =>
            {
                try
                {
                    return photonView.IsMine;
                }
                catch
                {
                    return SO_DependencyManager.Instance.CreatePlayerOffline;
                }
            };
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
            IsMine = () => photonView.IsMine;
            if (createRoomOnPhotonJoinedLobby)
                CreateRoom();
        }
        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomName))
            {
                Debug.LogWarning("Room name is empty");
                return;
            }
            PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 3 }, TypedLobby.Default);
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Log("Room Creation Failed: " + message);
            OnPhotonFailedToConnect?.Invoke(message);
        }
        public void Log(string msg)
        {
            if (ShowDebug)
                Debug.Log(msg);
        }
        #endregion

        public override void OnCreatedRoom()
        {
            Log("Created Room");
            OnPhotonCreatedRoom?.Invoke(PhotonNetwork.CurrentRoom.Name);
        }
        public override void OnJoinedRoom()
        {
            Log("Joined Room");
            Log("Room name: " + PhotonNetwork.CurrentRoom.Name);
            OnPhotonJoinedRoom?.Invoke(PhotonNetwork.CurrentRoom.Name);
        }
        public override void OnLeftRoom()
        {
            Log("Left Room");
            OnPhotonLeftRoom?.Invoke();
        }
    }
}
