using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneAttackState : EnemyState
{
    protected override void EnterState()
    {
        _enemy.AnimCompo.PlayAnimaton(AnimationType.attack);
        _enemy.AnimCompo.OnAnimationAction.AddListener(PerfromAttack);
        _enemy.AnimCompo.OnAnimationEnd.AddListener(TransitionState);
    }

    private void TransitionState()
    {
        _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Idle));
    }

    private void PerfromAttack()
    {
        Vector3 direction = _enemy.Target.transform.position - transform.position;
        direction.z = 0;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Instantiate(_enemy.EnemyData.bullet.transform, _enemy.transform.position, Quaternion.Euler(0,0,angle));
    }

    protected override void ExitState()
    {
        _enemy.AnimCompo.ResetEvent();
    }
}
