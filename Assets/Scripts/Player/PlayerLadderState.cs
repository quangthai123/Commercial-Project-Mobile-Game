using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLadderState : PlayerStates
{
    public PlayerLadderState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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
        rb.gravityScale = 0f;
        if(verticalInput == 0)
        {
            player.anim.SetBool("LadderMove", false);
            rb.velocity = Vector2.zero;
        }
        if(verticalInput != 0)
        {
            player.anim.SetBool("LadderMove", true);
            if(player.stateMachine.currentState != player.jumpState)
                rb.velocity = new Vector2(0f, player.ladderMoveSpeed * verticalInput);
        }
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.jumpState);
    }
}
