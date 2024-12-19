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

    private DragJump _drag;
    public override void Enter()
    {
        base.Enter();
        _drag = _player.GetCompo<DragJump>();
        _input = _player.GetCompo<InputReader>();
        _input.OnLeftPerform += HandleLeftClick;
        _input.OnLeftCanceled += HandleLeftCanceled;
    }
    public override void Exit()
    {
        base.Exit();
        if (_drag.isDrag)
            _drag.DragStop(() => _input.MouseWorldPos);
        _input.OnLeftPerform -= HandleLeftClick;
        _input.OnLeftCanceled -= HandleLeftCanceled;
    }
    protected override void HandleMouse()
    {
        base.HandleMouse();
    }
    private void HandleLeftCanceled()
    {
        if (_drag.isDrag)
        {
            _player.GetCompo<AgentVFX>().ToggleAfterImage(true);
            _drag.DragEnd(() => _input.MouseWorldPos);
            _stateMachine.ChangeState(PlayerEnum.Jump);
        }
    }

    private void HandleLeftClick()
    {
        if (CheckPlayerOverlaped(_input.MouseWorldPos))
            _drag.DragStart(() => _input.MouseWorldPos);
    }
    public bool CheckPlayerOverlaped(Vector2 pos)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, 0.4f);
        foreach (var item in colliders)
        {
            if (item.TryGetComponent(out Player player))
                return true;
        }
        return false;
    }
}
