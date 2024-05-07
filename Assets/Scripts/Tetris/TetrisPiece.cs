using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Controls the logic for each individual shape/piece
public class TetrisPiece : MonoBehaviour
{
    public TetrisBoard board { get; private set; }
    public Vector3Int position { get; private set; }
    public TetrominoData data { get; private set; }

    public Vector3Int[] cells { get; private set; }

    public void Initialize(TetrisBoard board, Vector3Int position, TetrominoData data) {
        this.board = board;
        this.position = position;
        this.data = data;

        if (this.cells == null) {
            this.cells = new Vector3Int[data.cells.Length];
        }

        for (int i = 0; i < data.cells.Length; i++) {
            this.cells[i] = (Vector3Int) data.cells[i];
        }
    }

    private void Update() {
        this.board.Clear(this);

        // Gets input from player to control the movement of the blocks
        if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.LeftArrow)) {
            Move(Vector2Int.left);
        }

        else if (Input.GetKeyDown(KeyCode.D)|| Input.GetKeyDown(KeyCode.RightArrow)) {
            Move(Vector2Int.right);
        }

        this.board.Set(this);
    }

    private bool Move(Vector2Int translation) {
        Vector3Int newPosition = this.position;
        newPosition.x += translation.x;
        newPosition.y += translation.y;

        bool valid = this.board.IsValidPosition(this, newPosition);

        if (valid) {
            this.position = newPosition;
        }

        return valid;
    }
}
