using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEditor.XR;

public class PhotonRoomManager : MonoBehaviourPunCallbacks
{
    public LocalLoginManager loginManager;
    public UIController uiController;

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Photon.");
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public void CreateRoom()
    {
        if (!loginManager.IsAdmin())
        {
            Debug.LogWarning("Only admin can create a room.");
            return;
        }

        string roomName = loginManager.GetRoomNameAdmin();
        if (string.IsNullOrEmpty(roomName)) return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
        PhotonNetwork.CreateRoom(roomName, options);
    }

    public void JoinRoom()
    {
        string roomName = loginManager.GetRoomNameGuest();
        if (string.IsNullOrEmpty(roomName)) return;

        PhotonNetwork.JoinRoom(roomName);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("Failed to join room: " + message);
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if (!loginManager.IsAdmin())
        {
            Debug.Log("Admin left. Leaving room...");
            PhotonNetwork.LeaveRoom();
        }
    }
    public override void OnLeftRoom()
    {
        bool wasAdmin = loginManager.IsAdmin();
        uiController.ReturnToMainMenu(wasAdmin);
    }
}
