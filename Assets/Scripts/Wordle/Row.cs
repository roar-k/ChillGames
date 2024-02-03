using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Row : MonoBehaviour
{
    // Array of the tiles
    public Tile[] tiles { get; private set; }

    private void Awake() {
        tiles = GetComponentsInChildren<Tile>();
    }
}
