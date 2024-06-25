using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerAirState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Start()
    {
        base.Start();
        stateDuration = player.jumpDuration;
    }
    public override void Exit()
    {
        base.Exit();
        //rb.velocity = Vector3.zero; 
    }
    public override void Update()
    {
        base.Update();
        if(stateDuration > 0) 
            rb.velocity = new Vector2(horizontalInput * player.moveSpeed, player.jumpForce);
        else 
            rb.velocity = new Vector2(horizontalInput * player.moveSpeed, rb.velocity.y);
        if (Input.GetKeyUp(KeyCode.Space) && player.jumpMinTime <= player.jumpDuration - stateDuration)
            stateDuration = -1;
        if (stateDuration < 0 && rb.velocity.y < -.1f)
        {
            stateMachine.ChangeState(player.fallState);
        }
        rb.gravityScale = 6f;
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
