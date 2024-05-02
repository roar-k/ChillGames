using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Flappy : MonoBehaviour
{
    public FlappyPlayer player;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject playButton;
    public GameObject gameOver;

    private int score;

    private void Awake() {
        Application.targetFrameRate = 60;

        Pause();
    }

    public void Play() {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        // Destroys all existing pipes when game starts
        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    // Pauses the game
    public void Pause() {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void GameOver() {
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }


}
