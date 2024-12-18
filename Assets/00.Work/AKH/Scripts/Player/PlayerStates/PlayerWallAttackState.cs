using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallAttackState : PlayerState
{
    public PlayerWallAttackState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
}
