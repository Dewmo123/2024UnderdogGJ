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
    public SpriteRenderer SpriteRenderer { get; protected set; }

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
        Agent = GetComponentInChildren<NavMeshAgent>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StateCompo.InitializeState(this);
        _currentHealth = EnemyData.maxHp;
        Agent.speed = EnemyData.moveSpeed;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        // Target = FindAnyObjectByType<Player>();
    }



    private void Start()
    {
        SetupEnemy();
    }

    public void SetupEnemy()
    {
        TransitionState(StateCompo.GetState(StateType.Move));
    }

    public void TakeDamage(float damage)
    {
        if (CurrentState == StateCompo.GetState(StateType.Death)) return;

        _currentHealth = Mathf.Clamp(_currentHealth -= damage, 0, EnemyData.maxHp);
        if (_currentHealth == 0)
            TransitionState(StateCompo.GetState(StateType.Death));
        else
            TransitionState(StateCompo.GetState(StateType.Hit));
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }
}
