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
    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();
        //if (player.CheckGrounded())
            rb.velocity = Vector2.zero;
    }
    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (horizontalInput != 0 || verticalInput != 0)
            stateMachine.ChangeState(player.runState);
    }
}
