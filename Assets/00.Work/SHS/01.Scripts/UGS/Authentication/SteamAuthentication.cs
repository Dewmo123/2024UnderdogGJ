using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class SteamAuthentication : Authentication
{
    string steamTicket;
    string steamId;
    protected override Task TryAuth()
    {
        try
        {
            return AuthenticationService.Instance.SignInWithSteamAsync(steamTicket, steamId);
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return Task.CompletedTask;
        }
    }
}
