using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDroneAttackState : EnemyState
{
    private float _createCoolTime = 0.3f;
    private float _createCount = 2;

    protected override void EnterState()
    {
        StartCoroutine(CreateCoroutine());
    }

    IEnumerator CreateCoroutine()
    {
        for (int i = 0; i < _createCount; i++)
        {
            yield return new WaitForSeconds(_createCoolTime);
            Instantiate(DataManager.Instance._bombEnemy, _enemy.transform.position, Quaternion.identity);
        }
        _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Idle));
    }
}
