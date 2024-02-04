using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Script for the tiles in 2048, added to the Tile prefab
public class Tiles : MonoBehaviour
{
    public TileState state { get; private set; }
    public TileCell cell { get; private set; }
    public int number { get; private set; }

    public bool locked { get; set; }

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

    // Spawns a tile at a random cell
    public void Spawn(TileCell cell) {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        transform.position = cell.transform.position;
    }

    // Animates the tile when it moves
    public void MoveTo(TileCell cell) {
        if (this.cell != null) {
            this.cell.tile = null;
        }
        this.cell = cell;
        this.cell.tile = this;

        StartCoroutine(Animate(cell.transform.position, false));
    }

    public void Merge(TileCell cell) {
        if (this.cell != null) {
            this.cell.tile = null;
        }

        // Sets cell to null because cell will be merged and destroyed
        this.cell = null;
        // Locks the cell so that tiles don't double merge
        cell.tile.locked = true;

        StartCoroutine(Animate(cell.transform.position, true));

    }

    // Animates the tile movement
    private IEnumerator Animate(Vector3 to, bool merging) {
        float elapsed = 0f;
        float duration = 0.1f;

        Vector3 from = transform.position;

        while (elapsed < duration) {
            transform.position = Vector3.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = to;

        // Destroys tile if it is merging
        if (merging) {
            Destroy(gameObject);
        }
    }
}
