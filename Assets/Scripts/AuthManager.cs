using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

// Script for handling the UI and signing up/ signing in in the "SignInScreen" scene

public class AuthManager : MonoBehaviour
{
    [Header("Screens")]
    public GameObject signInDisplay;

    [Header("Input")]
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    [Header("Messages")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI errorMessageText;

    public float displayErrorDuration = 5f;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    // Signs the player up based on input username/password
    public async void SignUp() {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;

        await SignUpWithUsernamePassword(usernameText, passwordText);

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
        }
    }

    // Signs the player in based on input username/password
    public async void SignIn() {
        string usernameText = usernameInput.text;
        string passwordText = passwordInput.text;

        await SignInWithUsernamePassword(usernameText, passwordText);

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
        }
    }

    // Signs the player in anonymously (guest account)
    public async void Guest() {
        await SignInAnonymouslyAsync();

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
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

    // Allows for anonymous sign in
    private async Task SignInAnonymouslyAsync() {
        try {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
            messageText.text = "Successfully signed in.";
            ShowSuccessMessage();
        }

        // Notifies players when signing in anonymously fails
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
}
