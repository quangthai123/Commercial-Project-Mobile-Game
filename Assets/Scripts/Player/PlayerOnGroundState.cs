using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnGroundState : PlayerStates
{
    public PlayerOnGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }
    public override void Start()
    {
        base.Start();
        player.doubleJumped = false;
        player.attackState.airAttackCounter = 1;
        player.airDashState.airDashed = false;
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        if (!player.CheckGrounded())
            stateMachine.ChangeState(player.fallState);
        if(player.CheckCeilling())
            stateMachine.ChangeState(player.crouchState);
    }
    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if(player.CheckGrounded() && Input.GetKey(KeyCode.Space))
            stateMachine.ChangeState(player.jumpState);
        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.shieldState);
        if (Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.attackState);
        if(Input.GetKeyDown(KeyCode.LeftShift) && Time.time - player.dashTimer > player.dashCooldown)
        {
            player.dashTimer = Time.time;
            stateMachine.ChangeState(player.dashState);
        }
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.healState);
        if (Input.GetKeyDown(KeyCode.S))
            stateMachine.ChangeState(player.enterCrouchState);
    }
}
