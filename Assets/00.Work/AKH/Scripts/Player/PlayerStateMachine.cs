using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerStateMachine
{
    public Dictionary<PlayerEnum, PlayerState> stateDictionary = new Dictionary<PlayerEnum, PlayerState>();
    public PlayerState currentState;
    public PlayerState beforeState;

    private Player _player;
    public void Init(PlayerEnum start, Player player)
    {
        ChangeState(start);
        _player = player;
    }
    public PlayerEnum GetCurType()
    {
        foreach (var value in stateDictionary)
        {
            if (value.Value == currentState)
                return value.Key;
        }
        return PlayerEnum.None;
    }
    public void ChangeState(PlayerEnum type)
    {
        if (currentState != null)
        {
            beforeState = currentState;
            currentState.Exit();
        }
        currentState = stateDictionary[type];
        currentState.Enter();
    }
    public void AddState(PlayerEnum type, PlayerState state)
    {
        stateDictionary.Add(type, state);
    }
}
