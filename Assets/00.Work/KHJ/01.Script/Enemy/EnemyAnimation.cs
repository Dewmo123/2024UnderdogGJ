using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyAnimation : MonoBehaviour
{
    public Animator Animator { get; private set; }


    public UnityEvent OnAttackAnimationEnd;
    public UnityEvent OnAttackAnimationAction;
    private void Awake()
    {
        Animator = GetComponent<Animator>();
    }

    protected void Play(string animName)
    {
        Animator.Play(animName);
    }

    public void Stop()
    {
        Animator.enabled = false;
    }


    public void PlayAnimaton(AnimationType animtype)
    {
        switch (animtype)
        {
            case AnimationType.idle:
                Play("Idle");
                break;
            case AnimationType.run:
                Play("Run");
                break;
            case AnimationType.attack:
                Play("Attack");
                break;
            case AnimationType.hit:
                Play("Hit");
                break;
            case AnimationType.death:
                Play("Death");
                break;
            default:
                break;
        }
    }

    internal void InvokeAnimationAttack() // 애니메이션 액션시 호출
    {
        OnAttackAnimationAction?.Invoke();
    }

    public void ResetEvent()
    {
        OnAttackAnimationAction.RemoveAllListeners();
    }
}
public enum AnimationType
{
    idle,
    run,
    attack,
    death,
    hit,
}