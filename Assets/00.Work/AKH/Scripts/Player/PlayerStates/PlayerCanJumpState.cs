using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCanJumpState : PlayerCanAttackState
{
    private InputReader _input;
    public PlayerCanJumpState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }


    public override void Enter()
    {
        base.Enter();
        _input = _player.GetCompo<InputReader>();
        _input.OnLeftPerform += HandleLeftClick;
        _input.OnLeftCanceled += HandleLeftCanceled;
    }
    public override void Exit()
    {
        base.Exit();
        if (_player.GetCompo<DragJump>().isDrag)
            _player.GetCompo<DragJump>().DragEnd(() => _input.MouseWorldPos);
        _input.OnLeftPerform -= HandleLeftClick;
        _input.OnLeftCanceled -= HandleLeftCanceled;
    }
    protected override void HandleMouse()
    {
        base.HandleMouse();
    }
    private void HandleLeftCanceled()
    {
        _stateMachine.ChangeState(PlayerEnum.Jump);
    }

    private void HandleLeftClick()
    {
        _player.GetCompo<DragJump>().DragStart(() => _input.MouseWorldPos);
    }
}
