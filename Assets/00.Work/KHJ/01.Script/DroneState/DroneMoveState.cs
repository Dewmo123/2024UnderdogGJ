using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneMoveState : EnemyState
{ 
    protected override void EnterState()
    {
        _enemy.Agent.updateRotation = false;
        _enemy.Agent.updateUpAxis = false;
        _enemy.AnimCompo.PlayAnimaton(AnimationType.run);
    }


    public override void StateFixedUpdate()
    {
        float d = Vector2.Distance(transform.position, _enemy.Target.position);
        if (d < _enemy.EnemyData.attackRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, _enemy.Target.position - transform.position, d, _enemy.WellMask);
            if (!ray)
            {
                _enemy.Agent.SetDestination(transform.position);
                _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Attack));
                return;
            }
        }
       _enemy.Agent.SetDestination(_enemy.Target.position);
    }
}
