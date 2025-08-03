using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ConnectionManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField inputFieldAdmin;
    [SerializeField] private TMP_InputField inputFieldGuest;

    public void CreateRoom()
    {
        string roomName = inputFieldAdmin.text;

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Room name cannot be empty.");
            return;
        }

        NetworkManager.Instance.CreateSession(roomName);
    }


    public void JoinRoom()
    {
        string roomName = inputFieldGuest.text;

        if (string.IsNullOrEmpty(roomName))
        {
            Debug.LogWarning("Room name cannot be empty.");
            return;
        }

        // Try joining room
        PhotonNetwork.JoinRoom(roomName);
    }

    // Implement callback
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"Failed to join room: {message}");
    }


}
