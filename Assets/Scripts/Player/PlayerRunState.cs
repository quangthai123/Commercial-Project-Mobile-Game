using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerOnGroundState
{
    public PlayerRunState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(player.moveSpeed * horizontalInput, 0f);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (horizontalInput == 0 && verticalInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
