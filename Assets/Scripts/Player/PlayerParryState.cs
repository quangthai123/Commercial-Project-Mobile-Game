using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerStates
{
    public PlayerParryState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        if (!player.isStrongStrike)
            stateDuration = player.parryDuration;
        else
            stateDuration = player.strongParryDuration;
    }
    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
        rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        //if (player.CheckGroundedWhileHurtOrParry())
        //    rb.velocity = new Vector2(player.pushBackSpeed * -player.facingDir, 0f);
        //else
        if(!player.CheckGroundedWhileHurtOrParry())
            rb.velocity = Vector2.zero;
        if (stateDuration < 0f)
            stateMachine.ChangeState(player.idleState);
    }
}
