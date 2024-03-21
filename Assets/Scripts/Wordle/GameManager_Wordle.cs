using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Wordle : MonoBehaviour
{
    public Board board;
    public CanvasGroup statistics;
    public CanvasGroup pauseMenu;

    public TextMeshProUGUI currentText;
    public TextMeshProUGUI highText;
    public TextMeshProUGUI wordListText;

    private int score;
    private string wordList;

    private void Start() {
        NewGame();
    }

    public void NewGame() {
        highText.text = LoadHiscore().ToString() + " Letter Words";

        statistics.alpha = 0f;
        pauseMenu.alpha = 0f;

        board.enabled = true;
    }

    public void GameOver() {
        board.enabled = false;

        highText.text = LoadHiscore().ToString() + " Letter Words";
        wordListText.text = LoadWords().ToString();

        StartCoroutine(Fade(statistics, 1f, 0.5f));

        statistics.interactable = true;
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

    // Closes Statistics UI
    public void CloseStatistics() {
        statistics.interactable = false;
        statistics.alpha = 0f;
    }

    // Opens Statistics UI
    public void OpenStatistics() {
        if (statistics.alpha == 0f) {
            statistics.interactable = true;
            statistics.alpha = 1f;

            ClosePauseMenu();
        }

        else {
            CloseStatistics();
        }
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
    private int LoadHiscore() {
        return PlayerPrefs.GetInt("highestlevel", 0);
    }

    // Add the current solved word to the word list box
    public void AddWord(string word) {
        this.wordList += word + "  ";

        wordListText.text = wordList.ToString();

        SaveWords();
    }

    // Saves all the words in the words solved box
    private void SaveWords() {
        string words = LoadWords();

        PlayerPrefs.SetString("words", wordList);
    }

    // Loads all the past words solved using Player Prefs
    private string LoadWords() {
        return PlayerPrefs.GetString("words", wordList);
    }

    // Closes the Pause Menu
    public void ClosePauseMenu() {
        pauseMenu.interactable = false;
        pauseMenu.alpha = 0f;
    }

    // Opens the Pause menu
    public void OpenPauseMenu() {
        // Only opens Pause menu if it is not currently opened, if not, it will close
        if (pauseMenu.alpha == 0f) {
            pauseMenu.interactable = true;
            pauseMenu.alpha = 1f;

            CloseStatistics();
        }

        else {
            ClosePauseMenu();
        }
    }
}
