using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerStates
{
    public PlayerDeathState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        player.isKnocked = true;
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.hitGroundedEffect, new Vector2(player.centerEffectPos.position.x - player.facingDir * .8f, player.centerEffectPos.position.y), Quaternion.identity);
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if (player.CheckGrounded())
            rb.velocity = Vector2.zero;
        if (finishAnim)
            GameManager.Instance.ResetGame();
    }
}
