using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLedgeGrabState : PlayerStates
{
    public PlayerLedgeGrabState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        //player.transform.position = player.ledgeCheckPos.position;
        rb.gravityScale = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.W))
            stateMachine.ChangeState(player.ledgeClimbState);
        if (Input.GetKeyDown(KeyCode.S))
        {
            player.canGrabLedge = false;
            stateMachine.ChangeState(player.fallState);
        }
    }
}
