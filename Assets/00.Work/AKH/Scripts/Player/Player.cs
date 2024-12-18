using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Settings
    [Header("Settings")]
    public float moveSpeed = 5f;
    public float jumpPower = 7f;
    #endregion
    #region Components
    public Rigidbody2D Rigid { get; private set; }
    public Animator Anim { get; private set; }
    #endregion
    private PlayerStateMachine _stateMachine;
    private Dictionary<Type, IPlayerComponent> _components;
    [SerializeField] private InputReader _inputReader;
    private void Awake()
    {
        Anim = GetComponentInChildren<Animator>();
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
        _stateMachine.Init(PlayerEnum.Idle, this);
        #endregion
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
}