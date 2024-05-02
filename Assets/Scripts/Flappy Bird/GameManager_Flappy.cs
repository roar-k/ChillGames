using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Flappy : MonoBehaviour
{
    private int score;

    public void GameOver() {
        Debug.Log("Game Over");
    }

    public void IncreaseScore() {
        score++;
    }


}
