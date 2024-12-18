using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum PlayerEnum
{
    WallIdle,
    Idle,
    Walk,
    Jump,
    Aim,
    Fall,
    Dead,
    Attack,
    WallAim,
    WallShoot,
    None
}
public abstract class PlayerState
{
    protected Player _player;
    protected PlayerStateMachine _stateMachine;
    protected int _animBoolHash;
    protected bool _endTriggerCalled;
    public PlayerState(PlayerStateMachine stateMachine, string animName, Player player)
    {
        _stateMachine = stateMachine;
        _animBoolHash = Animator.StringToHash(animName);
        _player = player;
    }

    public virtual void Enter()
    {
        _player.GetCompo<PlayerAnimator>().EnterAnimation(_animBoolHash);
        _endTriggerCalled = false;
    }
    public virtual void Exit()
    {
        _player.GetCompo<PlayerAnimator>().ExitAnimation(_animBoolHash);
    }
    public virtual void UpdateState()
    {

    }
    public void AnimationEndTrigger()
    {
        _endTriggerCalled = true;
    }
}
