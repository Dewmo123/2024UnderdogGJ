using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallAttackState : PlayerState
{
    public PlayerWallAttackState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerEnum.WallIdle);
    }
    public override void Exit()
    {
        base.Exit();
        _player.Rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
