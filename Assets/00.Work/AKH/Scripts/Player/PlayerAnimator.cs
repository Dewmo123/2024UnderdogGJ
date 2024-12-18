using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    private Animator _anim;
    private Player _player;
    public void Initialize(Player player)
    {
        _player = player;
        _anim = _player.Anim;
    }
    public void EnterAnimation(int hash)
    {
        _anim.SetBool(hash, true);
    }
    public void ExitAnimation(int hash)
    {
        _anim.SetBool(hash, false);
    }
    public void EndTriggerCalled()
    {
        _player.EndTriggerCalled();
    }
}
