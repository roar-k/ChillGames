using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Statistics : MonoBehaviour
{
    
    public Board board;
    public GameManager_Wordle gameManager;

    [Header("Numbers")]
    public TextMeshProUGUI rowOneGuess;
    public TextMeshProUGUI rowTwoGuess;
    public TextMeshProUGUI rowThreeGuess;
    public TextMeshProUGUI rowFourGuess;
    public TextMeshProUGUI rowFiveGuess;
    public TextMeshProUGUI rowSixGuess;

    private int score;
    private int rowNumber;

    private void Update() {
        if (board.enabled == false) {
            rowNumber = board.count;
            Debug.Log(rowNumber);
        }
    }

    // Sets the score to the current store
    private void SetScoreOne(int score) {
        this.score = score;

        SaveHiscoreOne();
    } 

    // Saves the highscore for first row guesses into player prefs
    private void SaveHiscoreOne() {
        int hiscoreRowOne = LoadHiscoreOne();

        if (score > hiscoreRowOne) {
            PlayerPrefs.SetInt("hiscoreRowOne", score);
        }
    }

    // Loads the player's highscore for first row guesses
    public int LoadHiscoreOne() {
        return PlayerPrefs.GetInt("hiscoreRowOne", 0);
    }
}
