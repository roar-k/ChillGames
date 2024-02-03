using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Row[] rows;
    
    private string[] solutions;
    private string[] validWords;
    private string word;
    private int rowIndex;
    private int columnIndex;

    [Header("States")]
    public Tile.State emptyState;
    public Tile.State occupiedState;
    public Tile.State correctState;
    public Tile.State wrongSpotState;
    public Tile.State incorrectState;

    private void Awake() {
        rows = GetComponentsInChildren<Row>();
    }

    private void Start() {
        LoadData();
        SetRandomWord();
    }
    // Loads all the words
    private void LoadData() {
        // Loads all valid words
        TextAsset textFile = Resources.Load("official_wordle_all") as TextAsset;
        validWords = textFile.text.Split('\n');

        // Loads all possible solutions
        textFile = Resources.Load("official_wordle_common") as TextAsset;
        solutions = textFile.text.Split('\n');
    }

    // Sets a random word as the solution from the array
    private void SetRandomWord() {
        word = solutions[Random.Range(0, solutions.Length)];
        word = word.ToLower().Trim();
    }

    private void Update() {
        Row currentRow = rows[rowIndex];

        // Backspacing or deleting letters
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            currentRow.tiles[columnIndex].SetLetter('\0');
            currentRow.tiles[columnIndex].SetState(emptyState);
        }

        // Checks to see if out of bounds, if yes, submit row
        else if (columnIndex >= currentRow.tiles.Length) {
            if (Input.GetKeyDown(KeyCode.Return)) {
                SubmitRow(currentRow);
            }
        }

        else {
            // Loop through array to check for each letter
            for (int i = 0; i < SUPPORTED_KEYS.Length; i++ ) {
                if (Input.GetKeyDown(SUPPORTED_KEYS[i])) {
                    // Checks to see what slot we are currently on
                    currentRow.tiles[columnIndex].SetLetter((char) SUPPORTED_KEYS[i]);
                    currentRow.tiles[columnIndex].SetState(occupiedState);
                    columnIndex++;
                    break;
                }
            }
        }
    }
    // Checks if submit word matches the solution
    private void SubmitRow(Row row) {
        // Sets the tiles to the corresponding state
        for (int i = 0; i < row.tiles.Length; i++) {
            Tile tile = row.tiles[i];
            if (tile.letter == word[i]) {
                tile.SetState(correctState);
            }
            else if (word.Contains(tile.letter)) {
                tile.SetState(wrongSpotState);
            }
            else {
                tile.SetState(incorrectState);
            }
        }

        rowIndex++;
        columnIndex = 0;

        // Disables script when run out of columns (temp)
        if (rowIndex >= rows.Length) {
            enabled = false;
        }
    }
}
