using UnityEngine;
using UnityEngine.Tilemaps;

// Script for the different pieces in tetris

public enum Tetromino
{
    // The letter code for each tetris shape/block called "Tetromino"
    // More info on Wikipedia, link: https://en.wikipedia.org/wiki/Tetromino
    I,
    O,
    T,
    J,
    L,
    S,
    Z,
}

[System.Serializable]
public struct TetrominoData {
    public TileBase tile;
    public Tetromino tetromino;
    public Vector2Int[] cells { get; private set; }

    public void Initialize() {
        cells = TetrisData.Cells[tetromino];
    }
}

