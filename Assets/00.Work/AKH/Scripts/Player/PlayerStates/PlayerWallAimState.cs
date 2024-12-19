using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallAimState : PlayerAimState
{
    public PlayerWallAimState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (_player.GetCompo<PlayerMovement>().isWall)
            _player.GetCompo<PlayerMovement>().HandleSpriteFlip(_player.transform.position + (_player.GetCompo<PlayerMovement>().IsFacingRight() ? Vector3.left : Vector3.right));
        _player.GetCompo<PlayerAnimator>().ChangeLayer(2);
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePosition;
    }
    public override void Exit()
    {
        base.Exit();
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected override void HandleFlip(Vector3 mousePos)
    {
    }
    protected override void HandleMouseCancel()
    {
        _player.GetCompo<BulletLine>().DisableLine();
        _stateMachine.ChangeState(PlayerEnum.WallShoot);
    }
}
