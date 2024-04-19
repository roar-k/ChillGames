using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

// Script to handle scores and starting a new game and game over
public class GameManager_DinoGame : MonoBehaviour
{
    public static GameManager_DinoGame Instance { get; private set; }

    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;
    public Button retryButton;
    public Button mainMenu;

    private DinoPlayer player;
    private Spawner spawner;

    private float score;

    private async void Awake() {
        await UnityServices.InitializeAsync();

        if (Instance == null) {
            Instance = this;
        }

        else {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy() {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start() {
        player = FindObjectOfType<DinoPlayer>();
        spawner = FindObjectOfType<Spawner>();

        NewGame();
    }

    public void NewGame() {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        // Destroys all existing obstacles every game
        foreach (var obstacle in obstacles) {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;
        enabled = true;

        // Turns on the player and object spawner when game starts
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);

        // Turns off the game over text and all buttons when game starts
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        mainMenu.gameObject.SetActive(false);

        UpdateHiscore();
    }

    public void GameOver() {
        gameSpeed = 0f;
        enabled = false;

        // Turns off the player and the object spawner when game is over
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);

        // Turns on game over text and all buttons when game is over
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        mainMenu.gameObject.SetActive(true);

        UpdateHiscore();
    }

    private void Update() {
        gameSpeed += gameSpeedIncrease * Time.deltaTime;
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
    }

    private void UpdateHiscore() {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        int lbscore = (int) hiscore;

        if (score > hiscore) {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            SubmitScore("shs_dino", lbscore);
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }

    public async void SubmitScore(string leaderboardId, float score) {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
    }

    private async Task AddPlayerScoreAsync(string leaderboardId, float score) {
        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        }

        catch (Exception e) {
            Debug.Log(e.Message);
            throw;
        }
    }
}

