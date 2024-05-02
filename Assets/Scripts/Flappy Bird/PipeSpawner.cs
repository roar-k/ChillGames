using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject prefab;
    public float spawnRate = 1f;
    public float minHeight = -1f;
    public float maxHeight = 1f;

    private void OnEnable() {
        // Repeatedly spawns pipes when script is enabled
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    // Stops spawning pipes when script is disabled
    private void OnDisable() {
        CancelInvoke(nameof(Spawn));
    }

    // Spawns pipes on screen
    private void Spawn() {
        GameObject pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
    }
}
