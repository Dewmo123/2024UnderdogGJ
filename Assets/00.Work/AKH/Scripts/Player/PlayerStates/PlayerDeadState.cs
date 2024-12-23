using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(PlayerStateMachine stateMachine, string animName, Player player) : base(stateMachine, animName, player)
    {
    }
    private RotateablePlayerVIsual _visual;
    private PlayerMovement _movement;
    private PlayerAnimator _anim;
    public override void Enter()
    {
        _movement = _player.GetCompo<PlayerMovement>();
        _visual = _player.GetCompo<RotateablePlayerVIsual>();
        _anim = _player.GetCompo<PlayerAnimator>();

        _visual.gameObject.SetActive(false);
        _anim.Renderer.enabled = true;
        _anim.ChangeAnimator(null, true);
        Time.timeScale = 1;
        base.Enter();
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if (_endTriggerCalled)
        {
            _endTriggerCalled = false;
            _player.OnDead?.Invoke();
        }
    }
}
