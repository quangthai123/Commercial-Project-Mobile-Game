using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKnockoutState : PlayerStates
{
    //private bool startCalled = false;
    public PlayerKnockoutState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = Vector2.zero;
        stateDuration = player.knockOutDuration;
        player.anim.ResetTrigger("GetUp");
        //startCalled = true;
    }
    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
        //startCalled = false;

    }
    public override void Update()
    {
        base.Update();
        //if (!startCalled)
        //    return;
        rb.velocity = Vector2.zero;
        if (stateDuration < 0f)
            player.anim.SetTrigger("GetUp");
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }
}
