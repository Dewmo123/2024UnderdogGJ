using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDroneAttackState : EnemyState
{
    private Vector2 targetPosition;
    private Color _color;

    private float _maxTime = 2.5f;
    private float _currentTime = 0;
    private bool _canAttack = true;

    protected override void EnterState()
    {
        _color = _enemy.SpriteRenderer.color;
        _enemy.SpriteRenderer.color = Color.red;
        _enemy.Agent.speed = _enemy.EnemyData.moveSpeed * 5f;
        Vector2 direction = (_enemy.Target.transform.position - _enemy.transform.position).normalized;
        float distance = Vector2.Distance(_enemy.Target.transform.position, _enemy.transform.position);
        targetPosition = (Vector2)_enemy.transform.position + direction * (distance * 2);
        print(targetPosition);
        _enemy.Agent.SetDestination(targetPosition);
    }

    public override void StateUpdate()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _maxTime)
        {
            _enemy.Agent.speed = _enemy.EnemyData.moveSpeed;
            _enemy.SpriteRenderer.color = _color;
            print("½Ã°£ ³¡");
            _currentTime = 0;
            _canAttack = true;
            _enemy.TransitionState(_enemy.StateCompo.GetState(StateType.Idle));
        }

        var over = Physics2D.OverlapCircle(_enemy.transform.position, 0.5f, _enemy.PlayerMask);
        if (over)
        {
            if (_canAttack&&over.TryGetComponent(out Player target))
            {
                target.GetCompo<PlayerHealth>().ChangeValue(-_enemy.EnemyData.damage);
                _canAttack = false;
            }
        }
    }
}
