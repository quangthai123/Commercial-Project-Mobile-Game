using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : PlayerStates
{
    public PlayerCrouchState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        player.normalCol.SetActive(false);
        player.dashCol.SetActive(true);
    }
    public override void Exit()
    {
        base.Exit();
        player.normalCol.SetActive(true);
        player.dashCol.SetActive(false);
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (horizontalInput != player.facingDir && horizontalInput != 0)
            player.Flip();
        if (!player.CheckCeilling() && !Input.GetKey(KeyCode.S))
            stateMachine.ChangeState(player.idleState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (!player.CheckCeilling() && Input.GetKeyUp(KeyCode.S))
            stateMachine.ChangeState(player.exitCrouchState);
        if (Input.GetKeyDown(KeyCode.LeftShift) && Time.time - player.dashTimer > player.dashCooldown)
        {
            player.dashTimer = Time.time;
            stateMachine.ChangeState(player.dashState);
        }
    }
}
