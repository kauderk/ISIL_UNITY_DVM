namespace Photon.Pun
{
    public sealed class LauncherNW : LauncherSetup
    {
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
