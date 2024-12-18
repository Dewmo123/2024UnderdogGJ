using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PlayerAimState : PlayerState
{
    public PlayerAimState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    private InputReader _input;
    private RotateablePlayerVIsual _visual;
    private PlayerMovement _movement;
    public override void Enter()
    {
        base.Enter();
        _movement = _player.GetCompo<PlayerMovement>();
        _visual = _player.GetCompo<RotateablePlayerVIsual>();
        _visual.gameObject.SetActive(true);
        _player.GetCompo<PlayerAnimator>().gameObject.SetActive(false);
        Time.timeScale = 0.5f;
        _input = _player.GetCompo<InputReader>();
        _input.OnRightCanceled += HandleMouseCancel;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Vector3 mousePos = _input.MouseWorldPos;
        Vector3 dir = mousePos - _visual.transform.position;
        HandleFlip(mousePos);

        float rotation = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (_movement.IsFacingRight())
            _visual.Rotate(rotation);
        else
            _visual.ReverseRotate(rotation);
    }

    private void HandleFlip(Vector3 mousePos)
    {
    }

    public override void Exit()
    {
        base.Exit();
        _player.GetCompo<RotateablePlayerVIsual>().gameObject.SetActive(false);
        _player.GetCompo<PlayerAnimator>().gameObject.SetActive(true);
        Time.timeScale = 1f;
        _input.OnRightCanceled -= HandleMouseCancel;
    }
    protected virtual void HandleMouseCancel()
    {
        _stateMachine.ChangeState(PlayerEnum.Attack);
    }
}
