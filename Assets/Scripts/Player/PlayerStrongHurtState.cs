using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrongHurtState : PlayerStates
{
    private bool felt;
    public PlayerStrongHurtState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        felt = false;
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.hitImpactEffect, player.hitEffectPos.position, Quaternion.identity);
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if (rb.velocity.y < -.1f)
            felt = true;
        if (felt && player.CheckGrounded() && !player.isDead)
            stateMachine.ChangeState(player.knockoutState);
        else if (felt && player.CheckGrounded() && player.isDead)
            stateMachine.ChangeState(player.deathState);
    }
}
