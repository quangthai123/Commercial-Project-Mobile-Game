using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirDashState : PlayerStates
{
    public bool airDashed = false;
    public PlayerAirDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        rb.gravityScale = 0f;
        stateDuration = player.dashDuration;
        //Spawner.instance.Spawn(player.transform.position, Quaternion.identity);
        PlayerEffectSpawner.instance.Spawn("airDashFx", player.leftEffectPos.position + new Vector3(0f, 1.2f, 0f), Quaternion.identity);
    }
    public override void Exit()
    {
        base.Exit();
        rb.gravityScale = 6f;
        airDashed = true;
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(player.dashSpeed * player.facingDir, 0f);
        if (stateDuration < 0f)
            stateMachine.ChangeState(player.fallState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (player.CheckWalled() && !player.CheckGrounded() && Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.wallSlideState);
    }
}
