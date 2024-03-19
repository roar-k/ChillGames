using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Wordle : MonoBehaviour
{
    public Board board;
    public CanvasGroup statistics;

    public TextMeshProUGUI currentText;
    public TextMeshProUGUI highText;

    private int score;
    private string words;

    private void Start() {
        NewGame();
    }

    public void NewGame() {
        SetScore(4);
        highText.text = LoadHiscore().ToString() + " Letter Words";

        statistics.enabled = true;
        statistics.alpha = 0f;
        statistics.interactable = false;

        board.enabled = true;
    }

    public void GameOver() {
        highText.text = LoadHiscore().ToString() + " Letter Words";
        
        statistics.interactable = true;
        StartCoroutine(Fade(statistics, 1f, 1f));

        board.enabled = false;
    }

    // Fading animation for statistics
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

    public void CloseStatistics() {
        statistics.interactable = false;
        statistics.alpha = 0f;
    }

    // Sets the score to the current store
    public void SetScore(int score) {
        this.score = score;

        currentText.text = score.ToString() + " Letter Words";

        SaveHiscore();
    }

    // Saves the highscore for first row guesses into player prefs
    private void SaveHiscore() {
        int highestlevel = LoadHiscore();

        if (score > highestlevel) {
            PlayerPrefs.SetInt("highestlevel", score);
        }
    }

    // Loads the player's highscore for first row guesses
    public int LoadHiscore() {
        return PlayerPrefs.GetInt("highestlevel", 0);
    }
}
