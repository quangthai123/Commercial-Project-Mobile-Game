using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallState : PlayerAirState
{
    public PlayerFallState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        player.landingDuration = Time.time;
        player.canLadder = true;
    }
    public override void Exit()
    {
        base.Exit();
        player.canLadder = false;
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(horizontalInput * player.moveSpeed, rb.velocity.y);
        if (player.CheckGrounded())
        {
            if (Time.time - player.landingDuration >= player.allowLandingTime)
            {
                stateMachine.ChangeState(player.landingState);
            } else
                stateMachine.ChangeState(player.idleState);
        }
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
