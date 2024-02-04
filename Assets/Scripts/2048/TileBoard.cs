using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the Board in 2048, added to Board object in 2048
public class TileBoard : MonoBehaviour
{
    public Tiles tilePrefab;
    public TileState[] tileStates;
    private TileGrid grid;
    private List<Tiles> tiles;

    private void Awake() {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tiles>(16);
    }

    // Creates two tiles on the board when game starts
    private void Start() {
        CreateTile();
        CreateTile();
    }

    // Default value of a tile is 2 (can change)
    private void CreateTile() {
        Tiles tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    // Moves tiles based on WASD or Arrow Keys in corresponding direction
    private void Update() {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
            MoveTiles(Vector2Int.up, 0, 1, 1, 1);
        }
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
            MoveTiles(Vector2Int.down, 0, 1, grid.height - 2, -1);
        }
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
            MoveTiles(Vector2Int.left, 1, 1, 0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
            MoveTiles(Vector2Int.right, grid.width - 2, -1, 0, 1);
        }
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY) {
        for (int x = startX; x >= 0 && x < grid.width; x += incrementX) {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY) {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied) {
                    MoveTile(cell.tile, direction);
                }
            }
        }
    }

    // Actual logic behind where the tile can move
    private void MoveTile(Tiles tile, Vector2Int direction) {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        // Checks if there is a tile in the moving direction
        while (adjacent != null) {
            // If there is a tile adjacent to the current tile, will merge if conditions correct
            if (adjacent.occupied) {
                // merging...
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null) {
            tile.MoveTo(newCell);
        }
    }
}
