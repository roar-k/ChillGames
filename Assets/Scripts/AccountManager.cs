using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

// Script for handling UI and changing name in "AccountScreen" scene

public class AccountManager : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField displayNameInput;
    public TMP_InputField currentPasswordInput;
    public TMP_InputField newPasswordInput;

    [Header("Messages")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI errorMessageText;

    public float displayErrorDuration = 8f;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    private void Start() {
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignInFailed += OnSignInFailed;
    }

    public async void ChangeName() {
        string displayNameText = displayNameInput.text;

        await UpdatePlayerNameAsync(displayNameText);
    }

    public async void ChangePassword() {
        string currentPasswordText = currentPasswordInput.text;
        string newPasswordText = newPasswordInput.text;

        await UpdatePasswordAsync(currentPasswordText, newPasswordText);
    }

    // When player signs in successfully
    private void OnSignedIn() {
        messageText.text = $"Signed in as: {AuthenticationService.Instance.PlayerId}";
    }

    // When player sign in fails
    private void OnSignInFailed(RequestFailedException e) {
        messageText.text = $"Sign in failed with exception: " + e.Message;
    }

    // Change the players name
    private async Task UpdatePlayerNameAsync(string playerName) {
        try {
            await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);
            messageText.text = "Name changed successfully!";
        }

        // Notifies players when there is an error with changing their name
        catch (AuthenticationException e) {
            ShowErrorMessage(e.Message);
        }

        catch (RequestFailedException e) {
            ShowErrorMessage(e.Message);
        } 
    }

    // Change the players password
    private async Task UpdatePasswordAsync(string currentPassword, string newPassword) {
        try {
            await AuthenticationService.Instance.UpdatePasswordAsync(currentPassword, newPassword);
            messageText.text = "Password changed sucessfully!";
        }

        // Notifies players when there is an error with changing their password
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

    public void GoMainMenu() {
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
        }
    }

    private void OnDestroy() {
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignInFailed -= OnSignInFailed;
    }
}
