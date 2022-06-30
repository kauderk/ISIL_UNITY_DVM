using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;
using UnityEditor;


namespace Networking
{
    public class LauncherNW : MonoBehaviourPunCallbacks
    {
        private string roomNameInputField;
        public void SetRoomNameInputField(string roomName) => roomNameInputField = roomName;


        public UnityEvent OnJoinedLobbyLauncher;
        public UnityEvent OnCreatedRoomLauncher;
        public UnityEvent<string> OnJoinedRoomLauncher;
        public UnityEvent<string> OnFailedToConnectLauncher;

        void Start()
        {
            Debug.Log("Conecting Photon PUN...");
            PhotonNetwork.ConnectUsingSettings();
        }
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to master");
            PhotonNetwork.JoinLobby();
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined lobby");
            OnJoinedLobbyLauncher?.Invoke(); // title
        }


        public void CreateRoom()
        {
            if (string.IsNullOrEmpty(roomNameInputField))
            {
                Debug.LogWarning("Room name is empty");
                return;
            }
            PhotonNetwork.CreateRoom(roomNameInputField);
            OnCreatedRoomLauncher?.Invoke(); // loading
        }
        // CreateRoom callbacks

        public override void OnJoinedRoom()
        {
            Debug.Log("Created room");
            OnJoinedRoomLauncher?.Invoke(PhotonNetwork.CurrentRoom.Name); // room
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("Room Creation Failed: " + message);
            OnFailedToConnectLauncher?.Invoke(message); // error
        }


        public void LeaveRoom()
        {
            PhotonNetwork.LeaveRoom();
            OnCreatedRoomLauncher?.Invoke(); // loading
        }
        public override void OnLeftRoom()
        {
            Debug.Log("Left room");
            OnJoinedLobbyLauncher?.Invoke(); // title
        }
    }


#if UNITY_EDITOR
    [CustomEditor(typeof(LauncherNW))]
    public class LauncherNWEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // centered header font 20
            EditorGUILayout.LabelField("Use SetRoomNameInputField() in other scripts to set the room name", EditorStyles.centeredGreyMiniLabel, GUILayout.Height(20));
            EditorGUILayout.Space();

            base.OnInspectorGUI();
        }
    }
#endif
}