using UnityEngine;

// Script for the Grid in 2048, added to the Grid object in 2048
public class TileGrid : MonoBehaviour
{
    public TileRow[] rows { get; private set; }
    public TileCell[] cells { get; private set; }

    public int size => cells.Length;
    public int height => rows.Length;
    public int width => size / height;

    private void Awake() {
        rows = GetComponentsInChildren<TileRow>();
        cells = GetComponentsInChildren<TileCell>();
    }

    private void Start() {
        for (int y = 0; y < rows.Length; y++) {
            for (int x = 0; x < rows[y].cells.Length; x++) {
                rows[y].cells[x].coordinates = new Vector2Int(x, y);
            }
        }
    }

    // Gets the coordinate of where the cell is currently located
    public TileCell GetCell(int x, int y) {
        if (x >= 0 && x < width && y >= 0 && y < height) {
            return rows[y].cells[x];
        }
        else {
            return null;
        }
    }

    public TileCell GetCell(Vector2Int coordinates) {
        return GetCell(coordinates.x, coordinates.y);
    }

    // Gets the cells next to the current cell
    public TileCell GetAdjacentCell(TileCell cell, Vector2Int direction) {
        Vector2Int coordinates = cell.coordinates;
        coordinates.x += direction.x;
        coordinates.y -= direction.y;

        return GetCell(coordinates);
    }

    // Finds a random empty cell in the board
    public TileCell GetRandomEmptyCell() {
        int index = Random.Range(0, cells.Length);
        int startingIndex = index;

        // If cell is occupied, moves on
        while (cells[index].occupied) {
            index++;

            if (index >= cells.Length) {
                index = 0;
            }

            if (index == startingIndex) {
                return null;
            }
        }

        return cells[index];
    }
}
