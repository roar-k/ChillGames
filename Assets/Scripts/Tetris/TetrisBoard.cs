using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

// Controls the overall/general state of Tetris
public class TetrisBoard : MonoBehaviour
{
    public Tilemap tilemap { get; private set; }
    public TetrominoData[] tetrominoes;

    private void Awake() {
        this.tilemap = GetComponentInChildren<Tilemap>();

        for (int i = 0; i < this.tetrominoes.Length; i++) {
            this.tetrominoes[i].Initialize();
        }
    }

    private void Start() {
        SpawnPiece();
    }

    // Spawns a random tetris block
    public void SpawnPiece() {
        int random = Random.Range(0, this.tetrominoes.Length);
        TetrominoData data = this.tetrominoes[random];
    }

    public void Set() {

    }
}
