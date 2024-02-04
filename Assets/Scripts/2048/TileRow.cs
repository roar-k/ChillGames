using UnityEngine;

// Script for the Row in 2048, added to the Row object in 2048
public class TileRow : MonoBehaviour
{
    public TileCell[] cells { get; private set; }

    private void Awake() {
        cells = GetComponentsInChildren<TileCell>();
    }
}
