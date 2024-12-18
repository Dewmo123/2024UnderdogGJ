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
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }
    public override void Exit()
    {
        base.Exit();
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    protected override void HandleMouseCancel()
    {
        _stateMachine.ChangeState(PlayerEnum.WallShoot);
    }
}
