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
    private int rowIndex;
    private int columnIndex;

    private void Awake() {
        rows = GetComponentsInChildren<Row>();
    }
    private void Update() {
        Row currentRow = rows[rowIndex];

        // Backspacing or deleting letters
        if (Input.GetKeyDown(KeyCode.Backspace)) {
            columnIndex = Mathf.Max(columnIndex - 1, 0);
            currentRow.tiles[columnIndex].SetLetter('\0');
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
                    columnIndex++;
                    break;
                }
            }
        }
    }
    // Submits the row when enter is pressed
    private void SubmitRow(Row row) {
        //...
    }
}
