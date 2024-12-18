using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    private InputReader _input;
    public override void Enter()
    {
        base.Enter();
        Time.timeScale = 0.5f;
        _input = _player.GetCompo<InputReader>();
        _input.OnRightCanceled += HandleMouseCancel;
    }
    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _input.OnRightCanceled -= HandleMouseCancel;
    }
    private void HandleMouseCancel()
    {
        _stateMachine.ChangeState(PlayerEnum.Attack);
    }
}
