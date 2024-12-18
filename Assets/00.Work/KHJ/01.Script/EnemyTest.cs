using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTest : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _range = 3;
    [SerializeField] private float _attackCool = 1;
    [SerializeField] private LayerMask _wellMask;

    private bool _canAttack = true;
    NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    private void Update()
    {
        if (!_canAttack) return;

        float d = Vector2.Distance(transform.position, _target.position);
        if (d < _range)
        {
            RaycastHit2D ray = Physics2D.Raycast(transform.position, _target.position - transform.position, d, _wellMask);
            if (!ray)
            {
                _agent.SetDestination(transform.position);

                if (_canAttack)
                {
                    Vector3 direction = _target.position - transform.position;
                    direction.z = 0;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    StartCoroutine(AttackCoroutine());
                }
                return;
            }
        }
        _agent.SetDestination(_target.position);
    }
    
    private IEnumerator AttackCoroutine()
    {
        _canAttack = false;
        yield return new WaitForSeconds(0.5f);
        print("АјАн");
        yield return new WaitForSeconds(_attackCool);
        _canAttack = true;

    }




    /*private void OnDrawGizmos()
    {
        float d = Vector2.Distance(transform.position, _target.position);
        Debug.DrawRay(transform.position, _target.position- transform.position, Color.red, d);
    }*/
}