using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtState : PlayerStates
{
    private bool felt;
    public PlayerHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        felt = false;
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.hitImpactEffect, player.transform.position, Quaternion.identity);
    }

    public override void Exit()
    {
        base.Exit();
        if (stateMachine.currentState != player.dashState)
            rb.velocity = Vector2.zero;
        player.isKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < -.1f)
            felt = true;
        if(!player.CheckGroundedWhileHurtOrParry())
            rb.velocity = new Vector2(0f, rb.velocity.y);
        if (felt && player.CheckGrounded())
            rb.velocity = Vector2.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }
}
