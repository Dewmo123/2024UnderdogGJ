using GGMPool;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour, IHittable
{
    #region Settings
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 7f;
    #endregion
    #region Components
    public Rigidbody2D Rigid { get; private set; }
    #endregion
    public UnityEvent OnTimeSlow;
    public UnityEvent OnTimeRecover;
    public UnityEvent OnJump;
    public UnityEvent OnWalk;
    public UnityEvent OnDead;

    public Transform FirePoint;

    private PlayerStateMachine _stateMachine;
    private Dictionary<Type, IPlayerComponent> _components;
    [SerializeField] private InputReader _inputReader;

    private void Awake()
    {
        Rigid = GetComponent<Rigidbody2D>();
        #region SetIplayerCompo
        _components = new Dictionary<Type, IPlayerComponent>();
        _components.Add(_inputReader.GetType(), _inputReader);
        GetComponentsInChildren<IPlayerComponent>().ToList().ForEach((item) => _components.Add(item.GetType(), item));
        _components.Values.ToList().ForEach((item) => item.Initialize(this));
        #endregion
        #region SetState
        _stateMachine = new PlayerStateMachine();
        _stateMachine.AddState(PlayerEnum.Idle, new PlayerIdleState(_stateMachine, "Idle", this));
        _stateMachine.AddState(PlayerEnum.Walk, new PlayerWalkState(_stateMachine, "Walk", this));
        _stateMachine.AddState(PlayerEnum.Aim, new PlayerAimState(_stateMachine, "Aim", this));
        _stateMachine.AddState(PlayerEnum.Attack, new PlayerAttackState(_stateMachine, "Attack", this));
        _stateMachine.AddState(PlayerEnum.Fall, new PlayerFallState(_stateMachine, "Fall", this));
        _stateMachine.AddState(PlayerEnum.Jump, new PlayerJumpState(_stateMachine, "Jump", this));
        _stateMachine.AddState(PlayerEnum.WallIdle, new PlayerWallIdleState(_stateMachine, "WallIdle", this));
        _stateMachine.AddState(PlayerEnum.WallAim, new PlayerWallAimState(_stateMachine, "WallAim", this));
        _stateMachine.AddState(PlayerEnum.WallShoot, new PlayerWallAttackState(_stateMachine, "WallShoot", this));
        _stateMachine.Init(PlayerEnum.Idle, this);
        #endregion
        GetCompo<PlayerHealth>().OnHealthDec += HandleHealthChanged;
    }
    private void OnDestroy()
    {
        GetCompo<PlayerHealth>().OnHealthDec -= HandleHealthChanged;
    }
    private void HandleHealthChanged(int val)
    {
        StartCoroutine(RecoverShader());
    }
    private IEnumerator RecoverShader()
    {
        GetCompo<PlayerAnimator>().Renderer.material.SetFloat("_Value", 1);
        yield return new WaitForSeconds(0.2f);
        GetCompo<PlayerAnimator>().Renderer.material.SetFloat("_Value", 0);
    }


    private void Update()
    {
        _stateMachine.currentState.UpdateState();
    }
    public void EndTriggerCalled()
    {
        _stateMachine.currentState.AnimationEndTrigger();
    }
    public T GetCompo<T>() where T : class
    {
        Type t = typeof(T);
        if (_components.TryGetValue(t, out var compo))
        {
            return compo as T;
        }
        return null;
    }
    public void OnHit(int damage)
    {
        GetCompo<PlayerHealth>().ChangeValue(-damage);
    }
}
