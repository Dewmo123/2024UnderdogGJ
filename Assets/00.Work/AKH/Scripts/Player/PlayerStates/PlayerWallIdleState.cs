using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallIdleState : PlayerCanJumpState
{
    public PlayerWallIdleState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.GetCompo<PlayerMovement>().HandleSpriteFlip(_player.transform.position + (_player.GetCompo<PlayerMovement>().IsFacingRight() ? Vector3.left : Vector3.right));
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }
    public override void Exit()
    {
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
        base.Exit();
    }
    protected override void HandleMouse()
    {
        _stateMachine.ChangeState(PlayerEnum.WallAim);
    }
}