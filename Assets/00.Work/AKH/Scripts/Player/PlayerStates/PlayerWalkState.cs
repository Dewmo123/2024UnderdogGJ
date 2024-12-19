using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerState
{
    private PlayerMovement movement;
    public PlayerWalkState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
        movement = _player.GetCompo<PlayerMovement>();
    }
    public override void UpdateState()
    {
        Vector2 moveVec = _player.GetCompo<InputReader>().MoveVector;
        if (moveVec == Vector2.zero)
            _stateMachine.ChangeState(PlayerEnum.Idle);
        movement.SetMovement(new Vector2(moveVec.x * _player.moveSpeed, _player.Rigid.velocity.y));
        movement.HandleSpriteFlip(moveVec + (Vector2)_player.transform.position);
    }
}
