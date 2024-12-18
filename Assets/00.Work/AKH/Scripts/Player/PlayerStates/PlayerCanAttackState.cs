using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanAttackState : PlayerState
{
    public PlayerCanAttackState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.GetCompo<InputReader>().OnRightPerform += HandleMouse;
    }
    public override void Exit()
    {
        base.Exit();
        _player.GetCompo<InputReader>().OnRightPerform -= HandleMouse;
    }

    private void HandleMouse()
    {
        _stateMachine.ChangeState(PlayerEnum.Aim);
    }
}
