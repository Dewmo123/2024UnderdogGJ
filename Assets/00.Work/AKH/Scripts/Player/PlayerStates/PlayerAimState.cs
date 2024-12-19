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
    private PlayerAnimator _anim;
    public override void Enter()
    {
        base.Enter();
        _movement = _player.GetCompo<PlayerMovement>();
        _visual = _player.GetCompo<RotateablePlayerVIsual>();
        _anim = _player.GetCompo<PlayerAnimator>();

        _visual.gameObject.SetActive(true);
        _anim.Renderer.enabled = false;
        _anim.ExitAnimation(_animBoolHash);
        _anim.ChangeAnimator(_visual.Anim);
        if (_player.Rigid.velocity != Vector2.zero)
            _anim.ChangeLayer(1);
        else
            _anim.ChangeLayer(0);
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

    protected virtual void HandleFlip(Vector3 mousePos)
    {
        if ((_movement.IsFacingRight() && mousePos.x < _player.transform.position.x) || (!_movement.IsFacingRight() && mousePos.x > _player.transform.position.x))
            _movement.HandleSpriteFlip(mousePos);
    }

    public override void Exit()
    {
        base.Exit();
        Time.timeScale = 1f;
        _input.OnRightCanceled -= HandleMouseCancel;
    }
    protected virtual void HandleMouseCancel()
    {
        _stateMachine.ChangeState(PlayerEnum.Attack);
    }
}
