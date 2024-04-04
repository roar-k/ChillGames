using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class AccountManager : MonoBehaviour
{
    [Header("Input")]
    public TMP_InputField displayNameInput;

    [Header("Messages")]
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI errorMessageText;

    public float displayErrorDuration = 5f;

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

    // Shows an error message there is an error with authentication
    public void ShowErrorMessage(string message) {
        errorMessageText.text = message;
        errorMessageText.gameObject.SetActive(true);
        Invoke("HideErrorMessage", displayErrorDuration);
    }

    private void HideErrorMessage() {
        errorMessageText.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignInFailed -= OnSignInFailed;
    }
}
