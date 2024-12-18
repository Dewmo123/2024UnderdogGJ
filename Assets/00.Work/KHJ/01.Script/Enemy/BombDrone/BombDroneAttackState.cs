using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombDroneAttackState : EnemyState
{
    protected override void EnterState()
    {
        _enemy.AnimCompo.PlayAnimaton(AnimationType.attack);
        _enemy.AnimCompo.OnAnimationAction.AddListener(PerfromAttack);
    }


    private void PerfromAttack()
    {
        StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(1);
        Instantiate(_enemy.EnemyData.bullet.transform, _enemy.transform.position, Quaternion.Euler(0, 0, 0)).GetComponent<Bomb>().PlayBomb();
        Destroy(_enemy.gameObject);
    }

    protected override void ExitState()
    {
        _enemy.AnimCompo.ResetEvent();
    }
}
