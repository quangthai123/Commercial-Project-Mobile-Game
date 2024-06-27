using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerOnGroundState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        if (!player.CheckSlope())
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(horizontalInput * player.moveSpeed, rb.velocity.y);
    }
    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (horizontalInput != 0)
            stateMachine.ChangeState(player.runState);
    }
}
