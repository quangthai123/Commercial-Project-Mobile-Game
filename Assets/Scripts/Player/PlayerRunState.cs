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
        if(!player.isKnocked)
            rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        rb.sharedMaterial = player.normalPhysicMat;
        if (!player.CheckSlope())
            rb.velocity = new Vector2(player.moveSpeed * horizontalInput, 0f);
        else
        {
            if(player.CheckJumpOnSlope())
                rb.velocity = new Vector2(player.moveSpeed * horizontalInput * -player.slopeMoveDir.x, player.moveSpeed * horizontalInput * -player.slopeMoveDir.y);
            //if (horizontalInput == 0)
            //    player.knockFlip = true;
            //else
            //    player.knockFlip = false;
        }
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (horizontalInput == 0)
            stateMachine.ChangeState(player.idleState);
    }
}
