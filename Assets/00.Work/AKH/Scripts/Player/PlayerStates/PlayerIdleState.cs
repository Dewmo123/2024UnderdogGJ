using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerCanJumpState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateState()
    {
        _player.GetCompo<PlayerMovement>().SetMovement(Vector2.zero);
        if (_player.GetCompo<InputReader>().MoveVector != Vector2.zero)
        {
            _stateMachine.ChangeState(PlayerEnum.Walk);
        }
    }
}
