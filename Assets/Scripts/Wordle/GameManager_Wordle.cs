using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager_Wordle : MonoBehaviour
{
    public Board board;
    public CanvasGroup leaderboard;

    private void Start() {
        NewGame();
    }

    public void NewGame() {
        leaderboard.alpha = 0f;
        leaderboard.interactable = false;

        board.enabled = true;
    }

    public void GameOver() {
        leaderboard.interactable = true;
        StartCoroutine(Fade(leaderboard, 1f, 1f));

        board.enabled = false;
    }

    // Fading animation for Leaderboard
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

    public void CloseLeaderboard() {
        leaderboard.interactable = false;
        leaderboard.alpha = 0f;
    }
}
