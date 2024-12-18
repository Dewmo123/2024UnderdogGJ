using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    public PlayerAttackState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    public override void UpdateState()
    {
        if (_endTriggerCalled && _player.Rigid.velocity != Vector2.zero)
            _stateMachine.ChangeState(PlayerEnum.Jump);
        else if (_endTriggerCalled)
            _stateMachine.ChangeState(PlayerEnum.Idle);
            
    }
}
