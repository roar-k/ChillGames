using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;

public class WordleLeaderboard : MonoBehaviour
{
    public string leaderboardId = "shs_wordle";
    public LeaderboardScoreView scoreViewPrefab;
    public GameObject leaderboardDisplay;

    public TextMeshProUGUI messageText;
    public TMP_InputField scoreInputField;
    public Button submitScoreButton;
    public Button loadScoresButton;
    public Transform scoresContainer;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    private async void Start() {
        /* AuthenticationService.Instance.SignedIn += OnSignedIn;
        AuthenticationService.Instance.SignInFailed += OnSignInFailed;

        messageText.text = "Signing in...";

        // Players sign in anonymously
        await AuthenticationService.Instance.SignInAnonymouslyAsync(); */

        submitScoreButton.onClick.AddListener(SubmitScoreAsync);
        loadScoresButton.onClick.AddListener(LoadScoresAsync);
    }

    private void Update() {
        bool isSignedIn = AuthenticationService.Instance.IsSignedIn;
        if (isSignedIn) {
            leaderboardDisplay.SetActive(true);
        }

        else {
            leaderboardDisplay.SetActive(false);
        }
    }

    // When player signs in successfully
    /* private void OnSignedIn() {
        messageText.text = $"Signed in as: {AuthenticationService.Instance.PlayerId}";
    }

    // When player sign in fails
    private void OnSignInFailed(RequestFailedException exception) {
        messageText.text = $"Sign in failed with exception: {exception}";
    } */

    private async void SubmitScoreAsync() {
        if (string.IsNullOrEmpty(scoreInputField.text)) {
            return;
        }

        var score = Convert.ToDouble(scoreInputField.text);
        scoreInputField.text = string.Empty;

        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
            messageText.text = "Score submitted!";
        }

        catch (Exception e) {
            messageText.text = $"Failed to submit score: {e}";
            throw;
        }
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
            messageText.text = $"Failed to fetch scores: {e}";
            throw;
        }
    }

    /* private void OnDestroy() {
        AuthenticationService.Instance.SignedIn -= OnSignedIn;
        AuthenticationService.Instance.SignInFailed -= OnSignInFailed;
    } */
}
