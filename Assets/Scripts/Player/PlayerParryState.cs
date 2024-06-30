using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParryState : PlayerStates
{
    private float spawnParryEffectTimer;
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
        spawnParryEffectTimer = player.spawnParryEffectCooldown;
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.parryEffect, player.centerEffectPos.position, Quaternion.identity);
        if(!player.isStrongStrike)
            PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.flashParryEffect, player.shieldEffectPos.position, Quaternion.identity);
        else
        {
            PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.strongParryEffect, player.shieldEffectPos.position, Quaternion.identity);
            PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.flashParryEffect2, player.shieldEffectPos.position, Quaternion.identity);
        }
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
        if (spawnParryEffectTimer < 0f && player.CheckGroundedWhileHurtOrParry())
        {
            spawnParryEffectTimer = player.spawnParryEffectCooldown;
            PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.parryEffect, player.centerEffectPos.position, Quaternion.identity);
        }
        else
            spawnParryEffectTimer -= Time.deltaTime;
        if(!player.CheckGroundedWhileHurtOrParry())
            rb.velocity = Vector2.zero;
        if (stateDuration < 0f)
            stateMachine.ChangeState(player.idleState);
    }
}
