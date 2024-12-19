using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMoveState : EnemyState
{
    protected override void EnterState()
    {
        _enemy.AnimCompo.PlayAnimaton(AnimationType.run);
    }


    public override void StateFixedUpdate()
    {
        float d = Vector2.Distance(transform.position, _enemy.Target.transform.position);
        if (d < _enemy.EnemyData.attackRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, _enemy.Target.transform.position - transform.position, d, _enemy.WellMask);
            if (!ray)
            {
                _enemy.Agent.SetDestination(transform.position);
                _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Attack));
                return;
            }
        }
        if (_enemy.Agent.enabled && _enemy.Agent.isOnNavMesh)
        {
            _enemy.Agent.SetDestination(_enemy.Target.transform.position);
        }
    }
}
