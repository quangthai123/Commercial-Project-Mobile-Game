using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnterCrouchState : PlayerStates
{
    public PlayerEnterCrouchState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = Vector2.zero;
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if (player.CheckGrounded())
            rb.velocity = Vector3.zero;
        else
            stateMachine.ChangeState(player.fallState);
        if (finishAnim)
            stateMachine.ChangeState(player.crouchState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyUp(KeyCode.S))
            stateMachine.ChangeState(player.exitCrouchState);
    }
}
