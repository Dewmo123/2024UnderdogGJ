using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkDroneMoveState : EnemyState
{
    public override void StateFixedUpdate()
    {
        float d = Vector2.Distance(transform.position, _enemy.Target.transform.position);
        if (d < _enemy.EnemyData.attackRange)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, _enemy.Target.transform.position - transform.position, d, _enemy.WellMask);
            if (!ray)
            {
                if (_enemy.Agent.enabled && _enemy.Agent.isOnNavMesh)
                    _enemy.Agent.SetDestination(transform.position);
                _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Attack));
                _enemy.SpriteRenderer.color = new Color(_enemy.SpriteRenderer.color.r, _enemy.SpriteRenderer.color.g, _enemy.SpriteRenderer.color.b, 1);
                return;
            }
        }
        else
        {
            _enemy.SpriteRenderer.color = new Color(_enemy.SpriteRenderer.color.r, _enemy.SpriteRenderer.color.g, _enemy.SpriteRenderer.color.b, 0f);
        }
        if (_enemy.Agent.enabled && _enemy.Agent.isOnNavMesh)
            _enemy.Agent.SetDestination(_enemy.Target.transform.position);
    }
}
