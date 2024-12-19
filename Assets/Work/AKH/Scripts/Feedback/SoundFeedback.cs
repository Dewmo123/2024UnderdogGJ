using System.Collections;
using System.Collections.Generic;
using GGMPool;
using UnityEngine;

public class SoundFeedback : Feedback
{
    [HideInInspector] public PoolTypeSO poolTypeSO;
    [SerializeField] private SoundSO soundSO;
    public override void PlayFeedback()
    {
        IPoolable poolable = PoolManagerSO.Instance.Pop(poolTypeSO);
        SoundPlayer soundPlayer = poolable as SoundPlayer;
        soundPlayer.PlaySound(soundSO);
    }

    public override void StopFeedback()
    {
        Debug.LogWarning("SoundFeedback: 응 아니야");
    }
}
