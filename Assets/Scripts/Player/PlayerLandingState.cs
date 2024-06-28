using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerStates
{
    public PlayerLandingState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        if(player.facingDir == 1)
        {
            PlayerEffectSpawner.instance.Spawn("groundedLeftFx", player.leftEffectPos.position, Quaternion.identity);
            PlayerEffectSpawner.instance.Spawn("groundedRightFx", player.rightEffectPos.position, Quaternion.identity);
        }
        else
        {
            PlayerEffectSpawner.instance.Spawn("groundedLeftFx", player.rightEffectPos.position, Quaternion.identity);
            PlayerEffectSpawner.instance.Spawn("groundedRightFx", player.leftEffectPos.position, Quaternion.identity);
        }
        PlayerEffectSpawner.instance.Spawn("lightGroundedFx", player.centerEffectPos.position, Quaternion.identity);
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
        if (!player.CheckSlope())
            rb.velocity = Vector2.zero;
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
    }
}
