using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script for the tiles in 2048, added to the Tile prefab
public class Tiles : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }

    private Image background;
    private TextMeshProUGUI text;

    private void Awake() {
        background = GetComponent<Image>();
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Sets the state of each tile
    public void SetState(TileState state, int number) {
        this.state = state;
        this.number = number;

        // Sets the color and number of the tile
        background.color = state.backgroundColor;
        text.color = state.textColor;
        text.text = number.ToString();
    }

    // Spawns a tile at a particular cell
    public void Spawn(TileCell cell) {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }

    public void MoveTo(TileCell cell) {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        // Animates the tile movement
        transform.position = cell.transform.position;
    }
}
