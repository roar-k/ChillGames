using System.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Authentication;
public class GameManager_Bee : MonoBehaviour
{
    public static GameManager_Bee Instance { get; private set; }

    private int score;

    private async void Awake() {
        await UnityServices.InitializeAsync();
    }

    public async void SubmitScore(string leaderboardId, float score) {
        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
    }

    private async Task AddPlayerScoreAsync(string leaderboardId, float score) {
        try {
            await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, score);
        }

        catch (Exception e) {
            Debug.Log(e.Message);
            throw;
        }
    }

    public void OpenBeeLeaderboard() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.BeeLeaderboard);
    }
}
