using UnityEngine;

// Script for the tile cells in 2048, added to cell object in 2048
public class TileCell : MonoBehaviour
{
    public Vector2Int coordinates { get; set; }
    public Tiles tile { get; set; }

    public bool empty => tile == null;
    public bool occupied => tile != null;
}
