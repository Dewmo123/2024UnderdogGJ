using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard_RegistUI : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text leaderBoardScoreText;
    [SerializeField] TMP_InputField nameInput;
    [ContextMenu("TestDrawScore")]
    public void TestDrawScore()
    {
        DrawScore(100);
    }
    public async void DrawScore(int score)
    {
        scoreText.text = score.ToString();

        LeaderboardScoresWithNotFoundPlayerIds value = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync("ranking", new List<string>() { AuthenticationService.Instance.PlayerId });
        leaderBoardScoreText.text = value.Results[0].Score.ToString();
    }
    public void RegistScore()
    {
        Leaderboard.Instance.AddValue(int.Parse(scoreText.text), nameInput.text);
    }
}
