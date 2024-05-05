using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class GameManager_Wordle : MonoBehaviour
{
    public Board board;
    public CanvasGroup statistics;

    public TextMeshProUGUI currentText;
    public TextMeshProUGUI highText;
    public TextMeshProUGUI wordListText;

    public int score;
    private string wordList;

    private void Start() {
        NewGame();
    }

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    public void NewGame() {
        highText.text = LoadHiscore().ToString() + " Letter Words";
        wordListText.text = "";

        statistics.alpha = 0f;

        board.enabled = true;
    }

    public void GameOver() {
        board.enabled = false;

        highText.text = LoadHiscore().ToString() + " Letter Words";
        wordListText.text = board.GetWord() + " ;)";

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
        return PlayerPrefs.GetInt("highestlevel", 4);
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

    // Submits the current level to the leaderboard
    public async void SubmitScore(string leaderboardId, double score) {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
    }

    private async Task AddPlayerScoreAsync(string leaderboardId, double score) {
        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        }

        catch (Exception e) {
            Debug.Log(e.Message);
            throw;
        }
    }
}
