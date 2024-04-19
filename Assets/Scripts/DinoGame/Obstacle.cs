using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float leftEdge;

    private void Start() {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 2f;
    }
    private void Update() {
        transform.position += Vector3.left * GameManager_DinoGame.Instance.gameSpeed * Time.deltaTime;

        // Destroys the obstacle when it goes out of view from the camera
        if (transform.position.x < leftEdge) {
            Destroy(gameObject);
        }
    }

    
}
