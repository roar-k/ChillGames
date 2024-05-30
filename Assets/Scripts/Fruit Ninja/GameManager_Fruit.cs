using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager_Fruit : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Image fadeImage;

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

    // Starts a new game and resets score back to 0
    private void NewGame() {
        Time.timeScale = 1f;

        blade.enabled = true;
        spawner.enabled = true;

        score = 0;
        scoreText.text = score.ToString();

        ClearScene();
    }

    // Clears the scene of all fruits and bombs
    private void ClearScene() {
        Fruit[] fruits = FindObjectsOfType<Fruit>();

        foreach (Fruit fruit in fruits) {
            Destroy(fruit.gameObject);
        }

        FruitBomb[] bombs = FindObjectsOfType<FruitBomb>();

        foreach (FruitBomb bomb in bombs) {
            Destroy(bomb.gameObject);
        }
    }

    // Increases score whenever fruit is sliced
    public void IncreaseScore() {
        score++;
        scoreText.text = score.ToString();
    }

    // Blade and fruits stop spawning when bomb is sliced, game over
    public void Explode() {
        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());
    }

    // Screen turns white when bomb is sliced and explodes
    private IEnumerator ExplodeSequence() {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration) {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.clear, Color.white, t);

            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        while (elapsed < duration) {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.white, Color.clear, t);

            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
