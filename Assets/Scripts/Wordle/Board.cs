using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script for the game board in Wordle, add to Board object in Wordle
public class Board : MonoBehaviour
{
    // Array of all the letters in the Alphabet
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.A, KeyCode.B, KeyCode.C, KeyCode.D, KeyCode.E, KeyCode.F, 
        KeyCode.G, KeyCode.H, KeyCode.I, KeyCode.J, KeyCode.K, KeyCode.L, 
        KeyCode.M, KeyCode.N, KeyCode.O, KeyCode.P, KeyCode.Q, KeyCode.R, 
        KeyCode.S, KeyCode.T, KeyCode.U, KeyCode.V, KeyCode.W, KeyCode.X, 
        KeyCode.Y, KeyCode.Z, 
    };

    public GameManager_Wordle gameManager;

    private Row[] rows;
    
    [Header("Words")]
    private string[] levelFourWords;
    private string[] levelFiveWords;
    private string[] levelSixWords;
    private string[] levelSevenWords;
    private string[] levelEightWords;
    private string[] validWords;
    public string word;

    private int rowIndex;
    private int columnIndex;
    public int count;
    public int level;
    private int completedCount;

    [Header("States")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    [Header("UI")]
    public GameObject invalidWordText;
    public Button mainMenuButton;
    public Button playAgainButton;
    public Button nextLevelButton;
    public GameObject statistics;

    private void Awake() {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start() {
        // Sets the level to the number of tiles in each row
        level = rows[0].tiles.Length;
        completedCount = 0;

        LoadData();
        SetRandomWord();
    }

    public void PlayAgain() {
        ClearBoard();
        SetRandomWord();
        gameManager.NewGame();

        enabled = true;
    }

    public void MainMenu() {
        //...
    }

    // Loads all the words
    private void LoadData() {
        // Loads all valid words
        TextAsset textFile = Resources.Load("valid_words") as TextAsset;
        validWords = textFile.text.Split('\n');

        // Loads all possible solutions
        textFile = Resources.Load("4_letter_words") as TextAsset;
        levelFourWords = textFile.text.Split('\n');

        textFile = Resources.Load("5_letter_words") as TextAsset;
        levelFiveWords = textFile.text.Split('\n');

        textFile = Resources.Load("6_letter_words") as TextAsset;
        levelSixWords = textFile.text.Split('\n');

        textFile = Resources.Load("7_letter_words") as TextAsset;
        levelSevenWords = textFile.text.Split('\n');

        textFile = Resources.Load("8_letter_words") as TextAsset;
        levelEightWords = textFile.text.Split('\n');
    }

    // Sets a random word as the solution from corresponding array 
    private void SetRandomWord() {
        if (level == 4) {
            word = levelFourWords[Random.Range(0, levelFourWords.Length)];
            word = word.ToLower().Trim();
        }

        if (level == 5) {
            word = levelFiveWords[Random.Range(0, levelFiveWords.Length)];
            word = word.ToLower().Trim();
        }

        if (level == 6) {
            word = levelSixWords[Random.Range(0, levelSixWords.Length)];
            word = word.ToLower().Trim();
        }

        if (level == 7) {
            word = levelSevenWords[Random.Range(0, levelSevenWords.Length)];
            word = word.ToLower().Trim();
        }

        if (level == 8) {
            word = levelEightWords[Random.Range(0, levelEightWords.Length)];
            word = word.ToLower().Trim();
        }
    }

    private void Update() {
        Row currentRow = rows[rowIndex];

        // Backspacing or deleting letters
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            columnIndex = Mathf.Max(columnIndex - 1, 0);

            currentRow.tiles[columnIndex].SetLetter('\0');
            currentRow.tiles[columnIndex].SetState(emptyState);

            invalidWordText.gameObject.SetActive(false);
        }

        // Checks to see if out of bounds, if yes and return is presssed, submit row
        else if (columnIndex >= currentRow.tiles.Length) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SubmitRow(currentRow);
            }
        }

        else {
            // Loop through array to check for each letter
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++ ) {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i])) {
                    // Sets the current slot to the key that is pressed
                    currentRow.tiles[columnIndex].SetLetter((char) SUPPORTED_KEYS[i]);
                    currentRow.tiles[columnIndex].SetState(occupiedState);
                    columnIndex++;
                    break;
                }
            }
        }
    }

    private void SubmitRow(Row row) {
        if (!IsValidWord(row.word)) {
            invalidWordText.gameObject.SetActive(true);
            return;
        }

        string remaining = word;

        for (int i = 0; i < row.tiles.Length; i++) {
            Tile tile = row.tiles[i];

            // Checks if the letter matches the solution
            if (tile.letter == word[i]) {
                tile.SetState(correctState);

                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }

            // Checks if the letter is completely incorrect
            else if (!word.Contains(tile.letter)) {
                tile.SetState(incorrectState);
            }
        }

        // Compares remaining letters to see if letters are correct but in wrong spot
        for (int i = 0; i < row.tiles.Length; i++) {
            Tile tile = row.tiles[i];

            if (tile.state != correctState && tile.state != incorrectState) {
                if (remaining.Contains(tile.letter)) {
                    tile.SetState(wrongSpotState);

                    int index = remaining.IndexOf(tile.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " " );
                }
                else {
                    tile.SetState(incorrectState);
                }
            }
        }

        // Disables script when player has won
        if (HasWon(row)) {
            count = rowIndex;
            gameManager.SetScore(level);
            gameManager.AddWord(word);

            // Player has to solve the level twice in order to move on to next level
            if (completedCount >= 1) {
                nextLevelButton.gameObject.SetActive(true);
            }

            completedCount++;
            enabled = false;
        }

        rowIndex++;
        columnIndex = 0;

        // Disables script when run out of rows
        if (rowIndex >= rows.Length) {
            enabled = false;
        }
    }
    
    // Loops through each tile on board and sets them to a null letter and empty state
    private void ClearBoard() {
        for (int row = 0; row < rows.Length; row++) {
            for (int col = 0; col < rows[row].tiles.Length; col++) {
                rows[row].tiles[col].SetLetter('\0');
                rows[row].tiles[col].SetState(emptyState);
            }
        }

        rowIndex = 0;
        columnIndex = 0;
    }

    // Checks to see if the word submitted is in the valid words list
    private bool IsValidWord(string word) {
        for (int i = 0; i < validWords.Length; i++) {
            if (validWords[i].ToLower().Trim() == word) {
                return true;
            }
        }
        return false;
    }

    // Checks if the player has won the game or not
    public bool HasWon(Row row) {
        for (int i = 0; i < row.tiles.Length; i++) {
            if (row.tiles[i].state != correctState) {
                return false;
            }
        }

        return true;
    }

    // Moves on to next level by switching scenes
    public void NextLevel() {
        if (level == 4) {
           ScenesManager.Instance.LoadScene(ScenesManager.Scene.WordleLvl2);
        }

        if (level == 5) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.WordleLvl3);
        }

        if (level == 6) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.WordleLvl4);
        }

        if (level == 7) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.WordleLvl5);
        }

        if (level == 8) {
            ScenesManager.Instance.LoadScene(ScenesManager.Scene.Wordle);
        }
    }

    // Main Menu and Play Again buttons disappear when script is enabled
    private void OnEnable() {
        mainMenuButton.gameObject.SetActive(false);
        playAgainButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        gameManager.NewGame();
    }

    // Main Menu and Play Again buttons appear when script is disabled
    private void OnDisable() {
        mainMenuButton.gameObject.SetActive(true);
        playAgainButton.gameObject.SetActive(true);
        gameManager.GameOver();
    }
}

