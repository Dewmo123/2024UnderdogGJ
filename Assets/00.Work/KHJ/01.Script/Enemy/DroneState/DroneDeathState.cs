using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DroneDeathState : EnemyState
{

    protected override void EnterState()
    {
        _enemy.OnDeath?.Invoke();
        _enemy.gameObject.layer = 0;
        _enemy.AnimCompo.PlayAnimaton(AnimationType.death);
        GameManager.Instance.KillEnemy(_enemy.EnemyData.score);
        Destroy(_enemy.gameObject);
        //_enemy.AnimCompo.OnAnimationEnd.AddListener(TransitionState);
    }
/*
    private void TransitionState()
    {
        _enemy.AnimCompo.ResetEvent();
        Destroy(_enemy.gameObject);
    }*/
}

