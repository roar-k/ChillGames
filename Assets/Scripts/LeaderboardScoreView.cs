using TMPro;
using UnityEngine;

public class LeaderboardScoreView : MonoBehaviour 
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;

    public void Initialize(string rank, string playerName, string score) {
        rankText.text = rank;
        nameText.text = playerName;
        scoreText.text = score;
    }
}