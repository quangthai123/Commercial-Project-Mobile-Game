using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerAirState
{
    private float enterWallJumpTime;
    private bool wallJumped = false;
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        enterWallJumpTime = Time.time;
        player.Flip();
        //player.knockFlip = true;
        rb.velocity = new Vector2(player.wallJumpForce.x * player.facingDir, player.wallJumpForce.y);
        wallJumped = false;
        //if(player.facingDir == 1)
        //{
        //    Debug.Log("Wall Jump Fx");
        //    PlayerEffectSpawner.instance.Spawn("startJumpFx", player.centerEffectPos.position, Quaternion.Euler(0f, -90f, 0f));
        //}
        //else
        //{
        //    Debug.Log("Wall Jump Fx");
        //    PlayerEffectSpawner.instance.Spawn("startJumpFx", player.centerEffectPos.position, Quaternion.Euler(0f, 90f, 0f));
        //}
    }
    public override void Exit()
    {
        base.Exit();
        player.knockFlip = false;
    }


    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < -0.1f)
            stateMachine.ChangeState(player.fallState);
        if (horizontalInput != 0 && horizontalInput != player.facingDir && Time.time - enterWallJumpTime >= player.allowWallJumpUpMinTime && Time.time - enterWallJumpTime <= player.allowWallJumpUpMaxTime && !wallJumped)
        {
            wallJumped = true;
            rb.velocity = new Vector2(player.wallJumpForce.x * -player.facingDir, player.wallJumpForce.y);
        }
        else if (horizontalInput != 0 && horizontalInput == player.facingDir)
            rb.velocity = new Vector2(horizontalInput * player.moveSpeed, rb.velocity.y);
        if (player.CheckWalled() && !player.CheckGrounded() && Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.wallSlideState);

    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
