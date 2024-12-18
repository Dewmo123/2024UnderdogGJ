using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerCanAttackState
{
    public PlayerIdleState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        if (_player.GetCompo<InputReader>().MoveVector != Vector2.zero)
        {
            _stateMachine.ChangeState(PlayerEnum.Walk);
        }
    }
}
