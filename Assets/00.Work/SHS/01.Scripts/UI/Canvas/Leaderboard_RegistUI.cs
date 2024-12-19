using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

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
    public void DrawScore()
    {
        DrawScore(GameManager.Instance.score);
    }
    public async void DrawScore(int score)
    {
        scoreText.text = score.ToString();
        try
        {
        LeaderboardScoresWithNotFoundPlayerIds value = await LeaderboardsService.Instance.GetScoresByPlayerIdsAsync("ranking", new List<string>() { AuthenticationService.Instance.PlayerId });
        leaderBoardScoreText.text = value.Results[0].Score.ToString();
        }
        catch
        {
            leaderBoardScoreText.text = "0";
        }
    }
    public void RegistScore()
    {
        Leaderboard.Instance.AddValue(int.Parse(scoreText.text), nameInput.text);
    }
}
