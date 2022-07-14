using System.Collections.Generic;
using UnityEngine;
namespace Photon.Pun
{
    [CreateAssetMenu(fileName = "PhotonLauncher", menuName = "Managers/PhotonLauncher")]
    public class SO_PhotonLauncher : SingletonScriptableObject<SO_PhotonLauncher>
    {
        public bool ShowDebug = true;

        [ReadOnly]
        public string roomName = "ISIL_DVM_Room";
        [field: SerializeField, BeginReadOnlyGroup]
        public List<string> roomList { get; private set; } = new List<string>();


        public string RoomName
        {
            get => roomName;
            set
            {
                roomName = value;
                // add to room list if the last room name is not the same as the new one
                if (roomList.Count > 0 && roomList[roomList.Count - 1] != roomName)
                    roomList.Add(roomName);
            }
        }
    }
}