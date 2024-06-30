using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealState : PlayerStates
{
    public PlayerHealState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        //PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.healEffect, player.centerEffectPos.position, Quaternion.identity);
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = Vector2.zero;
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
