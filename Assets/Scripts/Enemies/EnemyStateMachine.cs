using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyStates currentState;
    public void Initialize(EnemyStates _state)
    {
        currentState = _state;
        currentState.Start();
    }
    public void ChangeState(EnemyStates _state)
    {
        currentState.Exit();
        currentState = _state;
        currentState.Start();
    }
}
