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
        player.isShielding = false;
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
        if (horizontalInput != 0 && !Input.GetKeyDown(KeyCode.J) && !Input.GetKeyDown(KeyCode.K) && !Input.GetKeyDown(KeyCode.F)
            && !(Input.GetKeyDown(KeyCode.LeftShift) && Time.time - player.dashTimer > player.dashCooldown) && !(player.CheckGrounded() && Input.GetKey(KeyCode.Space)) && !(Input.GetKey(KeyCode.S) && !player.canLadder))
            stateMachine.ChangeState(player.runState);
    }
}
