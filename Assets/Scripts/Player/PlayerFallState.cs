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
        //rb.velocity = Vector3.zero;
        player.landingDuration = Time.time;
    }
    public override void Exit()
    {
        base.Exit();
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
        //if (player.CheckSlope())
        //{
        //    stateMachine.ChangeState(player.idleState);
        //}
        //    player.knockFlip = true;
        //    rb.velocity = Vector2.zero;
        //    rb.gravityScale = 0f;
        //}
        //else
        //{
        //    rb.gravityScale = 6f;
        //}
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
