using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneIdleState : EnemyState
{
    protected override void EnterState() // 공격 후 쿨타임동안 가만히 있음
    {
        if (_enemy.Agent.enabled && _enemy.Agent.isOnNavMesh)
            _enemy.Agent.SetDestination(transform.position);
        _enemy.AnimCompo.PlayAnimaton(AnimationType.idle);
        StartCoroutine(WaitAttackCool());
    }

    private IEnumerator WaitAttackCool()
    {
        yield return new WaitForSeconds(_enemy.EnemyData.attackCool);
        if (_enemy.CurrentState != _enemy.StateCompo.GetState(StateType.Death))
        {
            _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Move));
        }
    }
}
