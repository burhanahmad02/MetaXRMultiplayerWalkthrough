using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject roomCanvas;

    public GameObject adminMainPanel;
    public GameObject guestMainPanel;

    public void ShowRoomCanvas()
    {
        mainMenuCanvas.SetActive(false);
        roomCanvas.SetActive(true);
    }

    public void ReturnToMainMenu(bool isAdmin)
    {
        roomCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);

        adminMainPanel.SetActive(isAdmin);
        guestMainPanel.SetActive(!isAdmin);
    }
}
