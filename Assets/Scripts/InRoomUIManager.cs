using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.UI;

public class InRoomUIManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI welcomeText;
    public TextMeshProUGUI projectText;
    public GameObject adminPanel;

    public Button setProjectTextButton;
    public Button randomColorButton;
    public Button endSessionButton;

    private bool isAdmin = false;

    void Start()
    {
        isAdmin = PhotonNetwork.IsMasterClient;

        if (isAdmin)
        {
            welcomeText.text = "Welcome Admin";
            adminPanel.SetActive(true);
        }
        else
        {
            welcomeText.text = "Welcome Guest";
            adminPanel.SetActive(false);
        }

        setProjectTextButton.onClick.AddListener(SetProjectText);
        randomColorButton.onClick.AddListener(RandomizeColor);
        endSessionButton.onClick.AddListener(EndSession);
    }

    void SetProjectText()
    {
        photonView.RPC("RPC_SetProjectText", RpcTarget.AllBuffered, "This is a test project");
    }

    void RandomizeColor()
    {
        Color randomColor = new Color(Random.value, Random.value, Random.value);
        photonView.RPC("RPC_ChangeTextColor", RpcTarget.AllBuffered, randomColor.r, randomColor.g, randomColor.b);
    }

    void EndSession()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_EndSession", RpcTarget.All);
        }
    }

    [PunRPC]
    void RPC_EndSession()
    {
        PhotonNetwork.LeaveRoom();
    }


    [PunRPC]
    void RPC_SetProjectText(string text)
    {
        projectText.text = text;
    }

    [PunRPC]
    void RPC_ChangeTextColor(float r, float g, float b)
    {
        projectText.color = new Color(r, g, b);
    }

   

    public override void OnLeftRoom()
    {
        Debug.Log("Left room, loading appropriate panel...");
        // Load your admin or guest panel here depending on local role
        // For now, just log
        if (isAdmin)
            Debug.Log("Returning to Admin Panel");
        else
            Debug.Log("Returning to Guest Panel");
    }
}
