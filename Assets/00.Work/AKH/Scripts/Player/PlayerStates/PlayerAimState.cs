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

        if (Time.timeScale == 1)
        {
            Time.timeScale = 0.3f;
            _player.OnTimeSlow?.Invoke();
        }

        _player.GetCompo<BulletLine>().EnableLine();
        _input = _player.GetCompo<InputReader>();
        _input.OnRightCanceled += HandleMouseCancel;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        Rotate();
        if (_movement.isWall || _movement.ReverseWallCheck())
            _stateMachine.ChangeState(PlayerEnum.WallAim);
    }

    private void Rotate()
    {
        Vector3 mousePos = _input.MouseWorldPos;
        Vector3 shootVec = mousePos - _player.FirePoint.position;
        Vector3 visualVec = mousePos - _visual.transform.position;
        HandleFlip(mousePos);
        if (visualVec.magnitude < (_visual.transform.position - _player.FirePoint.position).magnitude * 2)
        {
            return;
        }
        float rotation = Mathf.Atan2(shootVec.y, shootVec.x) * Mathf.Rad2Deg;

        float angle = _player.GetCompo<RotateablePlayerVIsual>().Angle;
        Vector2 bulletDir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        _player.GetCompo<BulletLine>().DrawLine(bulletDir);

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
        _input.OnRightCanceled -= HandleMouseCancel;
    }
    protected virtual void HandleMouseCancel()
    {
        _player.GetCompo<BulletLine>().DisableLine();
        _stateMachine.ChangeState(PlayerEnum.Attack);
    }
}
