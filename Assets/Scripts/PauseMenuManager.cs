using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class PauseMenuManager : MonoBehaviour
{
    public CanvasGroup pauseMenu;
    public GameObject display;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    // Closes the Pause Menu
    public void ClosePauseMenu() {
        pauseMenu.gameObject.SetActive(false);
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0f;

        display.SetActive(true);
    }

    // Opens the Pause menu
    public void OpenPauseMenu() {
        // Only opens Pause menu if it is not currently opened, if not, it will close
        if (pauseMenu.alpha == 0f) {
            display.SetActive(false);

            pauseMenu.interactable = true;
            pauseMenu.gameObject.SetActive(true);
            pauseMenu.alpha = 1f;
        }

        else {
            ClosePauseMenu();
        }
    }

    // Signs the player out
    public async void SignOutOfGame() {
        await SignOut();

        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (!isSignedIn) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.SignInScreen);
        }
    }

    private async Task SignOut() {
        try {
            AuthenticationService.Instance.SignOut(true);
        }

        // Catches errors
        catch (AuthenticationException e) {
            Debug.Log(e.Message);
            throw;
        }

        catch (RequestFailedException e) {
            Debug.Log(e.Message);
            throw;
        }
    }

    public void OpenAccount() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.AccountScreen);
    }

    public void GoBack() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Leaderboard);
    }

}
