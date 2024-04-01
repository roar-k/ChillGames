using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    public void Awake() {
        Instance = this;
    }

    public enum Scene {
        SignInScreen,
        Leaderboard,
        Wordle,
        WordleLvl2,
        WordleLvl3,
        WordleLvl4,
        WordleLvl5
    }

    public void LoadScene(Scene scene) {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadNewGame() {
        SceneManager.LoadScene(Scene.Wordle.ToString());
    }

    public void LoadNextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    /* public void LoadMainMenu() {
        ScenesManager.LoadScene(Scene.MainMenu.ToString());
    } */
}
