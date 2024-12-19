using System.Collections;
using System.Collections.Generic;
using Unity.Services.Authentication;
using Unity.Services.Leaderboards;
using UnityEngine;

public class Leaderboard : MonoSingleton<Leaderboard>
{
    [SerializeField] string leaderboardId;
    [SerializeField] string playerName;
    [SerializeField] int value;
    [ContextMenu("AddValue")]
    public void AddValue()
    {
        AddValue(value, playerName);
    }
    public async void AddValue(int value, string playerName)
    {
        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName);

        AddPlayerScoreOptions options = new AddPlayerScoreOptions();

        await LeaderboardsService.Instance.AddPlayerScoreAsync(leaderboardId, value, options);
    }
}
