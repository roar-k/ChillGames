using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class GameManager_Flappy : MonoBehaviour
{
    public FlappyPlayer player;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject playButton;
    public GameObject leaderboardButton;
    public GameObject gameOver;

    private int score;

    private void Awake() {
        Application.targetFrameRate = 60;

        Pause();
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);
        leaderboardButton.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        // Destroys all existing pipes when game starts
        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    // Pauses the game
    public void Pause() {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        playButton.SetActive(true);
        leaderboardButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();

        SubmitScore("shs_bird", score);
    }

    // Changes to the BirdLeaderboard scene
    public void OpenLeaderboard() {
        Time.timeScale = 1f;
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.BirdLeaderboard);
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

}
