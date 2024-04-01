using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class AuthManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject signInDisplay;
    public GameObject displayNameDisplay;
    public CanvasGroup pauseMenu;

    [Header("Input")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField displayNameInput;

    [Header("Messages")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI errorMessageText;

    public float displayErrorDuration = 5f;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    /* private void Start() {
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            signInDisplay.SetActive(false);
        }
    }

    private void Update() {
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (!isSignedIn) {
            signInDisplay.SetActive(true);
        }
    } */

    // Signs the player up based on input username/password
    public async void SignUp() {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;

        await SignUpWithUsernamePassword(usernameText, passwordText);

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Leaderboard);
        }
    }

    // Signs the player in based on input username/password
    public async void SignIn() {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;

        await SignInWithUsernamePassword(usernameText, passwordText);

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Leaderboard);
        }
    }

    // Changes the players name based on their input
    public async void ChangeName() {
        string displayNameText = displayNameInput.text;

        await UpdatePlayerNameAsync(displayNameText);
    }

    // Signs the player out
    public async void SignOut() {
        await SignOutOfGame();
        ClosePauseMenu();
    }

    private async Task SignOutOfGame() {
        try {
            AuthenticationService.Instance.SignOut(true);
            messageText.text = "User is Signed Out!";
        }

        // Notifies players when signing out fails
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        }
    }

    // New players can sign up with username and password
    private async Task SignUpWithUsernamePassword(string username, string password) {
        try {
            await AuthenticationService.Instance.SignUpWithUsernamePasswordAsync(username, password);
            messageText.text = "Sign Up is successful!";
            ShowSuccessMessage();
        }

        // Notifies players when signing up fails
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        }
    }

    // Existing players can sign in with username and password
    private async Task SignInWithUsernamePassword(string username, string password) {
        try {
            await AuthenticationService.Instance.SignInWithUsernamePasswordAsync(username, password);
            messageText.text = "Sign In is successful!";
            ShowSuccessMessage();
        }

        // Notifies players when signing in fails
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        }
    }

    // Anonymous users can upgrade to username and password
    private async Task AddUsernamePasswordAsync(string username, string password) {
        try {
            await AuthenticationService.Instance.AddUsernamePasswordAsync(username, password);
            messageText.text = "Username and password added.";
            ShowSuccessMessage();
        }

        // Notifies players when adding username and password fails
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        }
    }

    // Change the players name
    private async Task UpdatePlayerNameAsync(string playerName) {
        try {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
        }

        // Notifies players when there is an error with changing their name
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        }
    }

    // Shows an error message there is an error with authentication
    public void ShowErrorMessage(string message) {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
        Invoke("HideErrorMessage", displayErrorDuration);
    }


    private void HideErrorMessage() {
        errorMessageText.gameObject.SetActive(false);
    }

    // Shows the success message
    public void ShowSuccessMessage() {
        messageText.gameObject.SetActive(true);
        Invoke("HideSuccessMessage", displayErrorDuration);
    }

    private void HideSuccessMessage() {
        messageText.gameObject.SetActive(false);
    }

    // Closes the Pause Menu
    public void ClosePauseMenu() {
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0f;
    }

    // Opens the Pause menu
    public void OpenPauseMenu() {
        // Only opens Pause menu if it is not currently opened, if not, it will close
        if (pauseMenu.alpha == 0f) {
            pauseMenu.interactable = true;
            pauseMenu.gameObject.SetActive(true);
            pauseMenu.alpha = 1f;
        }

        else {
            ClosePauseMenu();
        }
    }

    public void OpenChangeName() {
        displayNameDisplay.SetActive(true);
    }
}
