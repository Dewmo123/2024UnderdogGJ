using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateFactory : MonoBehaviour
{
    [SerializeField] protected EnemyState Idle, Move, Attack, Death, Hit;

    public virtual EnemyState GetState(StateType stateType)
        => stateType switch
        {
            StateType.Idle => Idle,
            StateType.Move => Move,
            StateType.Attack => Attack,
            StateType.Hit => Hit,
            StateType.Death => Death,
            _ => throw new System.Exception("aa")
        };

    public void InitializeState(Enemy enemy)
    {
        EnemyState[] states = GetComponents<EnemyState>();
        foreach (EnemyState state in states)
            state.InitializeState(enemy);
    }
}
public enum StateType
{
    Idle,
    Move,
    Attack,
    Hit,
    Death,
}