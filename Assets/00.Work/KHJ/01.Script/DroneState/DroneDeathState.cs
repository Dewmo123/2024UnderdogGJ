using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneDeathState : EnemyState
{
    protected override void EnterState()
    {
        _enemy.gameObject.layer = 0;
        _enemy.RbCompo.gravityScale = 10;
        _enemy.AnimCompo.PlayAnimaton(AnimationType.death);
        _enemy.AnimCompo.OnAnimationEnd.AddListener(TransitionState);
    }

    private void TransitionState()
    {
        _enemy.AnimCompo.ResetEvent();
        Destroy(_enemy.gameObject);
    }
}

