using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using UnityEngine;

public class AnonymousAuthentication : Authentication
{
    protected override Task TryAuth()
    {
        try
        {
            Task task = AuthenticationService.Instance.SignInAnonymouslyAsync();
            return task;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
            return Task.CompletedTask;
        }
    }
}
