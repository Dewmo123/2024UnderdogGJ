using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyAnimation AnimCompo { get; protected set; }
    public EnemyStateFectory StateCompo { get; protected set; }
    public EnemyState CurrentState;
    public Rigidbody2D RbCompo { get; protected set; }

    public Transform Target;

    public NavMeshAgent Agent { get; private set; }

    public LayerMask WellMask { get; private set; }

    public EnemyDataSO EnemyData;

    [SerializeField] private float _reFindTime = 0.5f;

    private float _currentHealth;


    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
        AnimCompo = GetComponentInChildren<EnemyAnimation>();
        StateCompo = GetComponentInChildren<EnemyStateFectory>();
        StateCompo.InitializeState(this);
        Agent = GetComponentInChildren<NavMeshAgent>();
        _currentHealth = EnemyData.maxHp;
    }



    private void Start()
    {
        SetupEnemy();
    }

    public void SetupEnemy()
    {
        TransitionState(StateCompo.GetState(StateType.Move));
    }

    private void Hit(int a, int b)
    {
        if (CurrentState == StateCompo.GetState(StateType.Death)) return;
    }

    private void Death()
    {
        TransitionState(StateCompo.GetState(StateType.Death));
    }

    public void TransitionState(EnemyState desireState)
    {
        if (desireState == null) return;

        if (CurrentState != null)
        {
            CurrentState.Exit();
        }
        CurrentState = desireState;
        CurrentState.Enter();
    }

    private void FixedUpdate()
    {
        CurrentState.StateFixedUpdate();
    }
}
