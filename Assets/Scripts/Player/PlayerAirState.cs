using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerStates
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        player.attackState.comboCounter = 0;
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if(Input.GetKeyDown(KeyCode.Space) && !player.doubleJumped)
        {
            player.doubleJumped = true;
            stateMachine.ChangeState(player.jumpState);
        }
        if (Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.attackState);
        if (Input.GetKeyDown(KeyCode.LeftShift) && !player.airDashState.airDashed)
            stateMachine.ChangeState(player.airDashState);
        if(player.CheckWalled() && !player.CheckGrounded() && Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.wallSlideState);
    }
}
