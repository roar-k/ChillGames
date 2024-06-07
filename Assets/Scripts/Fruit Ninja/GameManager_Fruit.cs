using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class GameManager_Fruit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image fadeImage;
    public GameObject gameOverScreen;
    public int health = 5;

    private Blade blade;
    private FruitSpawner spawner;

    private int score;

    private void Awake() {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<FruitSpawner>();
    }

    private void Start() {
        NewGame();
    }

    // Starts a new game and resets score back to 0
    public void NewGame() {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        gameOverScreen.SetActive(false);

        ClearScene();
    }

    // Clears the scene of all fruits and bombs
    private void ClearScene() {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }

        FruitBomb[] bombs = FindObjectsOfType<FruitBomb>();

        foreach (FruitBomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }

    // Game is over when health is 0 or when bomb is sliced
    private void GameOver() {
        ClearScene();

        gameOverScreen.SetActive(true);
    }

    // Increases score whenever fruit is sliced
    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
        SubmitScore("shs_fruit", score);
    }

    // Blade and fruits stop spawning when bomb is sliced, game over
    public void Explode() {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    // Screen turns white when bomb is sliced and explodes
    private IEnumerator ExplodeSequence() {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        ClearScene();

        elapsed = 0f;

        while (elapsed < duration) {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        GameOver();
    }

    // Subtracts a health when the fruit is not sliced
    public void MissedFruit() {
        if (health > 0) {
            health--;
        }

        else {
            GameOver();
        }

    }

    public async void SubmitScore(string leaderboardId, int score) {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
    }

    private async Task AddPlayerScoreAsync(string leaderboardId, int score) {
        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        }

        catch (Exception e) {
            Debug.Log(e.Message);
            throw;
        }
    }

    public void OpenFruitLeaderboard() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.FruitLeaderboard);
    }

    public void OpenMainMenu() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.MainMenu);
    }
}
