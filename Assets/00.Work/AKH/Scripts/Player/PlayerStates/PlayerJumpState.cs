using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerCanAttackState
{
    public PlayerJumpState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        _player.GetCompo<PlayerMovement>().HandleSpriteFlip(_player.transform.position + (Vector3)_player.Rigid.velocity);
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_player.Rigid.velocity.y <= 0)
            _stateMachine.ChangeState(PlayerEnum.Fall);
        if(_player.GetCompo<PlayerMovement>().isWall)
            _stateMachine.ChangeState(PlayerEnum.WallIdle);
    }
    protected override void HandleMouse()
    {
        base.HandleMouse();
    }
}
