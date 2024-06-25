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
        //if (player.CheckCeilling())
        //{
        //    stateMachine.ChangeState(player.crouchState);
        //    return;
        //}
        base.Start();
    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 6f;
    }
    public override void Update()
    {
        base.Update();
        //if (player.CheckGrounded())
            rb.velocity = Vector2.zero;
        if (player.CheckSlope())
        {
            rb.gravityScale = 0f;
            player.knockFlip = true;
        }
        else
        {
            rb.gravityScale = 6f;
            player.knockFlip = false;
        }
    }
    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (horizontalInput != 0)
            stateMachine.ChangeState(player.runState);
    }
}
