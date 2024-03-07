using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_Wordle : MonoBehaviour
{
    public Board board;
    public CanvasGroup statistics;

    private void Start() {
        NewGame();
    }

    public void NewGame() {
        statistics.alpha = 0f;
        statistics.interactable = false;

        board.enabled = true;
    }

    public void GameOver() {
        statistics.interactable = true;
        StartCoroutine(Fade(statistics, 1f, 1f));

        board.enabled = false;
    }

    // Fading animation for statistics
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay) {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration) {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }

    public void CloseStatistics() {
        statistics.interactable = false;
        statistics.alpha = 0f;
    }
}
