using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerCanAttackState
{
    public PlayerFallState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.GetCompo<PlayerMovement>().isGround)
            _stateMachine.ChangeState(PlayerEnum.Idle);
        if (_player.GetCompo<PlayerMovement>().isWall)
            _stateMachine.ChangeState(PlayerEnum.WallIdle); 
    }
    protected override void HandleMouse()
    {
        base.HandleMouse();
    }
}
