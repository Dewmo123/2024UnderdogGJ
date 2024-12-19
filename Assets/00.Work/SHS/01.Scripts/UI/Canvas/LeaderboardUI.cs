using System.Collections;
using System.Collections.Generic;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private Leaderboard_PlayerUI playerUIPrefab;
    [SerializeField] private Transform contentsTrm;
    [ContextMenu("DrawLeaderboard")]
    public async void DrawLeaderboard()
    {
        GetScoresOptions options = new GetScoresOptions();

        LeaderboardScoresPage leaderboardEntry = await LeaderboardsService.Instance.GetScoresAsync("ranking", options);

        List<LeaderboardEntry> ranks = leaderboardEntry.Results;

        ClearChildren(contentsTrm);

        foreach (LeaderboardEntry rank in ranks)
        {
            Leaderboard_PlayerUI playerUI = Instantiate(playerUIPrefab, contentsTrm);
            playerUI.SetPlayerUI(rank.PlayerName, rank.Score.ToString(), rank.PlayerId);
        }
    }
    public void ClearChildren(Transform transform)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }
}
