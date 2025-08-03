using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LocalLoginManager : MonoBehaviour
{
    [Header("Login UI")]
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public Button loginButton;

    [Header("Room UI")]
    public GameObject adminControls;
    public GameObject guestControls;
    public TMP_InputField createroomInput;
    public TMP_InputField joinroomInput;
    public Button createRoomButton;
    public Button joinRoomButton;

    private string adminEmail = "admin@example.com";
    private string adminPassword = "admin123";
    private bool isAdmin = false;

    void Start()
    {
        adminControls.SetActive(false);
    }

    public void TryLogin()
    {
        string email = emailInput.text.Trim();
        string password = passwordInput.text.Trim();

        if (email == adminEmail && password == adminPassword)
        {
            Debug.Log("Admin login successful.");
            isAdmin = true;
            adminControls.SetActive(true); // Show create room button
        }
        else
        {
            Debug.Log("Guest access.");
            isAdmin = false;
            guestControls.SetActive(true);
            adminControls.SetActive(false);
        }

        // Optional: Disable login UI after login
        emailInput.interactable = false;
        passwordInput.interactable = false;
        loginButton.interactable = false;
    }

    public bool IsAdmin()
    {
        return isAdmin;
    }

    public string GetRoomNameAdmin()
    {
        return createroomInput.text.Trim();
    }
    public string GetRoomNameGuest()
    {
        return joinroomInput.text.Trim();
    }
}
