using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerStates
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.parryEffect, player.centerEffectPos.position, Quaternion.identity);
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.strongParryEffect, player.shieldEffectPos.position, Quaternion.identity);
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.flashParryEffect2, player.shieldEffectPos.position, Quaternion.identity);
        player.isKnocked = true;
    }

    public override void Exit()
    {
        base.Exit();
        player.isKnocked = false;
    }

    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }
}
