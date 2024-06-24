using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerStates currentState;
    public void Initialize(PlayerStates _state)
    {
        currentState = _state;
        currentState.Start();
    } 
    public void ChangeState(PlayerStates _state)
    {
        currentState.Exit();
        currentState = _state;
        currentState.Start();
    }
}
