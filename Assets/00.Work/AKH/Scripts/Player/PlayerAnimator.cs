using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    private Animator _anim;
    private Player _player;
    private int _beforeLayer = 0;
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
    public void ChangeLayer(int index)
    {
        _anim.SetLayerWeight(index, 1);
        _anim.SetLayerWeight(_beforeLayer, 0);
        _beforeLayer = index;
    }
}
