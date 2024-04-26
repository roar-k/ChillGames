using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class BeeLeaderboard : MonoBehaviour
{
    public string leaderboardId = "shs_bee";
    public LeaderboardScoreView scoreViewPrefab;
    public GameObject leaderboardDisplay;

    [Header("Leaderboard UI")]
    public TextMeshProUGUI messageText;
    public Button loadScoresButton;
    public Transform scoresContainer;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    private void Start() {
        AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignInFailed += OnSignInFailed;

        loadScoresButton.onClick.AddListener(LoadScoresAsync);
    }

    // When player signs in successfully
    private void OnSignedIn() {
        messageText.text = $"Signed in as: {AuthenticationService.Instance.PlayerId}";
    }

    // When player sign in fails
    private void OnSignInFailed(RequestFailedException e) {
        messageText.text = $"Sign in failed with exception: " + e.Message;
    }

    private async void LoadScoresAsync() {
        try {
            var scoresResponse = await LeaderboardsService.Instance.GetScoresAsync(leaderboardId);
            var childCount = scoresContainer.childCount;

            for (int i = 0; i < childCount; i++) {
                Destroy(scoresContainer.GetChild(i).gameObject);
            }

            foreach (var leaderboardEntry in scoresResponse.Results) {
                var scoreView = Instantiate(scoreViewPrefab, scoresContainer);
                scoreView.Initialize(leaderboardEntry.Rank.ToString(), leaderboardEntry.PlayerName,
                    leaderboardEntry.Score.ToString());
            }

            // Notifies players when loading score is successfully completed
            messageText.text = "Scores fetched!";
        }
        
        // Notifies players when loading score fails
        catch (Exception e) {
            messageText.text = $"Failed to fetch scores: " + e.Message;
            throw;
        }
    }

    public void PlayBee() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.SpellingBee);
    }

    private void OnDestroy() {
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignInFailed -= OnSignInFailed;
    }
}
