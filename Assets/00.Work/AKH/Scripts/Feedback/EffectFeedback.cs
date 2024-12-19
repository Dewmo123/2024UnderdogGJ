using GGMPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFeedback : Feedback
{
    [SerializeField] private PoolManagerSO _manager;
    [SerializeField] private PoolTypeSO _type;
    private Player _player;
    private void Awake()
    {
        _player = transform.parent.GetComponentInParent<Player>();
    }
    public override void PlayFeedback()
    {
        Vector3 rotation;
        if (_player.BeforeState is PlayerWallIdleState)
            if (_player.GetCompo<PlayerMovement>().IsFacingRight())
                rotation = new Vector3(0, 0, 90);
            else
                rotation = new Vector3(0, 0, -90);
        else
            rotation = new Vector3(0, 0, 0);
        var s = _manager.Pop(_type) as Smoke;
        s.Play(transform, rotation);
    }

    public override void StopFeedback()
    {
    }
}
