using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour, IPlayerComponent
{
    private Animator localAnim;
    public Animator Animator { get; private set; }
    public SpriteRenderer Renderer { get; private set; }
    private Player _player;
    private int _beforeLayer = 0;
    public void Initialize(Player player)
    {
        _player = player;
        localAnim = GetComponent<Animator>();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = localAnim;
    }
    public void ChangeAnimator(Animator anim, bool isLocal = false)
    {
        if (isLocal)
        {
            Animator = localAnim;
            return;
        }
        Animator = anim;
    }
    public void EnterAnimation(int hash)
    {
        Animator.SetBool(hash, true);
    }
    public void ExitAnimation(int hash)
    {
        Animator.SetBool(hash, false);
    }
    public void EndTriggerCalled()
    {
        _player.EndTriggerCalled();
    }
    public void Attack()
    {
        float angle = _player.GetCompo<RotateablePlayerVIsual>().Angle;
        Vector2 dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        _player.Rigid.AddForce(-dir*_player.recoilPower, ForceMode2D.Impulse);
        _player.GetCompo<PlayerSniper>().Shoot(dir);
    }
    public void Walk()
    {
        _player.OnWalk?.Invoke();
    }
    public void ChangeLayer(int index)
    {
        Animator.SetLayerWeight(index, 1);
        Animator.SetLayerWeight(_beforeLayer, 0);
        _beforeLayer = index;
    }
}
