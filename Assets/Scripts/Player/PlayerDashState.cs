using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerStates
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        stateDuration = player.dashDuration;
        player.anim.ResetTrigger("DashStop");
        player.normalCol.SetActive(false);
        player.dashCol.SetActive(true);
    }
    public override void Exit()
    {
        base.Exit();
        player.normalCol.SetActive(true);
        player.dashCol.SetActive(false);
    }


    public override void Update()
    {
        base.Update();
        if(stateDuration > 0.07f && stateDuration < 0.1f && horizontalInput==0)
        {
            if (!player.CheckCeilling())
                player.anim.SetTrigger("DashStop");
            else
                stateMachine.ChangeState(player.crouchState);
        }
        else if (stateDuration < 0f)
        {
            if(horizontalInput == 0)
                rb.velocity = Vector2.zero;
            else if(!player.CheckCeilling())
                stateMachine.ChangeState(player.runState);
            else
                stateMachine.ChangeState(player.crouchState);
        } else
            rb.velocity = new Vector2(player.dashSpeed * player.facingDir, 0f);
        if(!player.CheckGrounded())
        {
            rb.velocity = Vector2.zero;
            stateMachine.ChangeState(player.fallState);
        }
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.jumpState);
        if(Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.shieldState);
    }
}
