using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerStates
{
    public PlayerLandingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();

    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        rb.velocity = Vector3.zero;
        rb.gravityScale = 0f;
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
