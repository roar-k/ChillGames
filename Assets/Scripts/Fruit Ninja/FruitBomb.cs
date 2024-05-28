using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitBomb : MonoBehaviour
{
    // Checks if the blade hits the bomb
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            FindObjectOfType<GameManager_Fruit>().Explode();
        }
    }
}
