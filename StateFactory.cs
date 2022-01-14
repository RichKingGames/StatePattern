using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateFactory : MonoBehaviour
{
    [SerializeField]
    private State Idle, Move, Jump, Fall, Attack, GetHit, Stun, Die;

    public State GetState(StateType type) => type switch
    {
        StateType.Idle => Idle,
        StateType.Move => Move,
        StateType.Jump => Jump,
        StateType.Fall => Fall,
        StateType.Attack => Attack,
        StateType.GetHit => GetHit,
        StateType.Stun => Stun,
        StateType.Die => Die, 
        _ => throw new System.Exception("State nor defined " + type.ToString())
    };

    internal void InitializeStates(Agent agent)
    {
        State[] states = GetComponents<State>();
        foreach (var state in states)
        {
            state.InitializeState(agent);
        }
    }
}
public enum StateType
{
    Idle, 
    Move, 
    Jump, 
    Fall, 
    Attack,
    GetHit, 
    Stun,
    Die
}
