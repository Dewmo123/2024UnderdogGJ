using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitState : EnemyState
{
    protected override void EnterState()
    {
        _enemy.AnimCompo.PlayAnimaton(AnimationType.hit);
        _enemy.AnimCompo.OnAnimationEnd.AddListener(TransitionState);
    }
    private void TransitionState()
    {
        _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Idle));
    }

    protected override void ExitState()
    {
        _enemy.AnimCompo.ResetEvent();
    }
}
