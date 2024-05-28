using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Fruit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private Blade blade;
    private FruitSpawner spawner;

    private int score;

    private void Awake() {
        blade = FindObjectOfType<Blade>();
        spawner = FindObjectOfType<FruitSpawner>();
    }

    private void Start() {
        NewGame();
    }

    private void NewGame() {
        score = 0;
        scoreText.text = score.ToString();
    }

    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }

    public void Explode() {
        blade.enabled = false;
        spawner.enabled = false;
    }
}
