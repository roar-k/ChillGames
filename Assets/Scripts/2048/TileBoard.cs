using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script for the Board in 2048, added to Board object in 2048
public class TileBoard : MonoBehaviour
{   
    public GameManager_2048 gameManager;
    public Tiles tilePrefab;
    public TileState[] tileStates;
    private TileGrid grid;
    private List<Tiles> tiles;
    private bool waiting;

    private void Awake() {
        grid = GetComponentInChildren<TileGrid>();
        tiles = new List<Tiles>(16);
    }

    // Clears the board when game is over
    public void ClearBoard() {
        foreach (var cell in grid.cells) {
            cell.tile = null;
        }

        foreach (var tile in tiles) {
            Destroy(tile.gameObject);
        }

        tiles.Clear();
        
    }

    // Default value of a tile is 2 (can change)
    public void CreateTile() {
        Tiles tile = Instantiate(tilePrefab, grid.transform);
        tile.SetState(tileStates[0], 2);
        tile.Spawn(grid.GetRandomEmptyCell());
        tiles.Add(tile);
    }

    // Moves tiles based on WASD or Arrow Keys in corresponding direction
    private void Update() {
        // Waits for the state of the tile to finish until animation begins again
        if (!waiting) {
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
    }

    private void MoveTiles(Vector2Int direction, int startX, int incrementX, int startY, int incrementY) {
        bool changed = false;

        for (int x = startX; x >= 0 && x < grid.width; x += incrementX) {
            for (int y = startY; y >= 0 && y < grid.height; y += incrementY) {
                TileCell cell = grid.GetCell(x, y);

                if (cell.occupied) {
                    changed |= MoveTile(cell.tile, direction);
                }
            }
        }

        if (changed) {
            StartCoroutine(WaitForChanges());
        }
    }

    // Checks for if adjacent is occupied to empty, moves or merges accordingly
    private bool MoveTile(Tiles tile, Vector2Int direction) {
        TileCell newCell = null;
        TileCell adjacent = grid.GetAdjacentCell(tile.cell, direction);

        // Checks if there is a tile in the moving direction
        while (adjacent != null) {
            // If there is a tile adjacent to the current tile, will merge if conditions correct
            if (adjacent.occupied) {
                if (CanMerge(tile, adjacent.tile)) {
                    Merge(tile, adjacent.tile);
                    return true;
                }
                break;
            }

            newCell = adjacent;
            adjacent = grid.GetAdjacentCell(adjacent, direction);
        }

        if (newCell != null) {
            tile.MoveTo(newCell);
            return true;
        }

        return false;
    }

    // Can only merge if the tiles are the same number
    private bool CanMerge(Tiles a, Tiles b) {
        return a.number == b.number && !b.locked;
    }

    // If CanMerge is true, deletes one tile and updates states of other tile
    private void Merge(Tiles a, Tiles b) {
        tiles.Remove(a);
        a.Merge(b.cell);

        /* Makes sure index will always stay in bound in the amount of Tile States created
         If 2 of the last Tile States merge, it will continue to stay at that last tile state
         In this case, 2048 is the last Tile State, so merging two 2048 tiles will make another
         2048 tile. Can increase further if additional Tile States are added
         */
        int index = Mathf.Clamp(IndexOf(b.state) + 1, 0, tileStates.Length - 1);
        int number = b.number * 2;

        b.SetState(tileStates[index], number);

        gameManager.IncreaseScore(number);
    }

    private int IndexOf(TileState state) {
        for (int i = 0; i < tileStates.Length; i++) {
            if (state == tileStates[i]) {
                return i;
            }
        }

        return -1;
    }

    // Waits for the state of the board to update before being able to move again
    private IEnumerator WaitForChanges() {
        waiting = true;
        
        yield return new WaitForSeconds(0.1f);

        waiting = false;

        foreach (var tile in tiles) {
            tile.locked = false;
        }

        if (tiles.Count != grid.size) {
            CreateTile();
        }

        if (CheckForGameOver()) {
            gameManager.GameOver();
        }
    }

    // When there are no more tiles to move in, Game is over!
    private bool CheckForGameOver() {
        if (tiles.Count != grid.size) {
            return false;
        }

        // When board is full, but tiles can still be merged, game is not over
        foreach (var tile in tiles) {
            TileCell up = grid.GetAdjacentCell(tile.cell, Vector2Int.up);
            TileCell down = grid.GetAdjacentCell(tile.cell, Vector2Int.down);
            TileCell left = grid.GetAdjacentCell(tile.cell, Vector2Int.left);
            TileCell right = grid.GetAdjacentCell(tile.cell, Vector2Int.right);

            if (up != null && CanMerge(tile, up.tile)) {
                return false;
            }

            if (down != null && CanMerge(tile, down.tile)) {
                return false;
            }

            if (left != null && CanMerge(tile, left.tile)) {
                return false;
            }

            if (right != null && CanMerge(tile, right.tile)) {
                return false;
            }
        }

        return true;
    }
}
