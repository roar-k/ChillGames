using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipes : MonoBehaviour
{
    public float speed = 5f;

    private float leftEdge;

    private void Start() {
        // Calculates the left edge of the screen
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
    }
    
    private void Update() {
        // Pipes constantly move to the left
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Destroys the pipes when the go past the left edge or the end of the screen
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }
}
