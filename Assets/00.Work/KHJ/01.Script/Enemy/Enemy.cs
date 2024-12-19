using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Enemy : MonoBehaviour, ISpawnable,IHittable
{
    [SerializeField] private SoundSO _drone;
    [SerializeField] private PoolManagerSO _poolManager;
    [SerializeField] private PoolTypeSO _sp;

    public UnityEvent OnDeath;
    public UnityEvent OnAttack;

    public EnemyAnimation AnimCompo { get; protected set; }
    public EnemyStateFactory StateCompo { get; protected set; }
    public EnemyState CurrentState;
    public Rigidbody2D RbCompo { get; protected set; }
    public SpriteRenderer SpriteRenderer { get; protected set; }

    public Player Target;

    public NavMeshAgent Agent { get; private set; }
    public LayerMask WellMask;
    public LayerMask PlayerMask;
    public EnemyDataSO EnemyData;

    private SoundPlayer _soundPlayer;


    private void Awake()
    {
        RbCompo = GetComponent<Rigidbody2D>();
        AnimCompo = GetComponentInChildren<EnemyAnimation>();
        StateCompo = GetComponentInChildren<EnemyStateFactory>();
        Agent = GetComponentInChildren<NavMeshAgent>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StateCompo.InitializeState(this);
        Agent.speed = EnemyData.moveSpeed;
        Agent.updateRotation = false;
        Agent.updateUpAxis = false;
        Target = FindObjectOfType<Player>();

    }



    private void Start()
    {
        _soundPlayer = _poolManager.Pop(_sp) as SoundPlayer;
        _soundPlayer.PlaySound(_drone);
        SetupEnemy();
    }

    public void SetupEnemy()
    {
        TransitionState(StateCompo.GetState(StateType.Move));
    }

    public void TakeDamage(float damage)
    {
        if (CurrentState != StateCompo.GetState(StateType.Death))
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

    private void Update()
    {
        CurrentState.StateUpdate();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(5);
        }
    }

    public void OnSpawn(Vector2 position)
    {
    }

    public void OnHit(int damage)
    {
        TakeDamage(damage);
    }
}
