using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Game Manage script for the 2048 game
public class GameManager_2048 : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    private int score;

    private void Start() {
        NewGame();
    }

    // Clears the board and adds 2 tiles onto board when New Game has started
    public void NewGame() {
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();

        gameOver.alpha = 0f;
        gameOver.interactable = false;
        
        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;
    }

    public void GameOver() {
        board.enabled = false;
        gameOver.interactable = true;

        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    public void MainMenu() {
        //...
    }

    // Fading animation for Game Over Screen
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay) {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    // Increases score whenever player successfully merges tiles
    public void IncreaseScore(int points) {
        SetScore(score + points);
    }

    // Sets the score box to the current score
    private void SetScore(int score) {
        this.score = score;

        scoreText.text = score.ToString();

        SaveHiscore();
    }

    // Sets the high score box to the high score
    private void SaveHiscore() {
        int hiscore = LoadHiscore();

        if (score > hiscore) {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    // Loads the player's highscore
    public int LoadHiscore() {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
}
