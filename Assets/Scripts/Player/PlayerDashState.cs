using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerStates
{
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        stateDuration = player.dashDuration;
        player.anim.ResetTrigger("DashStop");
        player.normalCol.SetActive(false);
        player.dashCol.SetActive(true);
        if (player.CheckSlope())
        {
            player.dashCol.GetComponent<CapsuleCollider2D>().enabled = true;
            player.dashCol.GetComponent<BoxCollider2D>().enabled = false;
        }
        //if (player.facingDir == 1)
        //{
        if(!player.CheckSlope()) 
            PlayerEffectSpawner.instance.Spawn("endDashFx", player.leftEffectPos.position, Quaternion.identity);
        //} else
        //    PlayerEffectSpawner.instance.Spawn("startDashFx", player.rightEffectPos.position, Quaternion.identity);
        player.isKnocked = true;
    }
    public override void Exit()
    {
        base.Exit();
        player.normalCol.SetActive(true);
        player.dashCol.GetComponent<CapsuleCollider2D>().enabled = false;
        player.dashCol.GetComponent<BoxCollider2D>().enabled = true;
        player.dashCol.SetActive(false);
        player.isKnocked = false;
        player.dashTimer = Time.time;
        //if(player.facingDir == 1)
        //{
        if (!player.CheckSlope() && player.CheckGrounded())
            PlayerEffectSpawner.instance.Spawn("startDashFx", player.rightEffectPos.position - player.facingDir * new Vector3(.3f, 0f, 0f), Quaternion.identity);
        //} else
        //{
        //    PlayerEffectSpawner.instance.Spawn("endDashFx", player.leftEffectPos.position, Quaternion.identity);
        //}
    }


    public override void Update()
    {
        base.Update();
        rb.sharedMaterial = player.normalPhysicMat;
        if (stateDuration > 0.07f && stateDuration < 0.1f && horizontalInput == 0)
        {
            if (!player.CheckCeilling())
                player.anim.SetTrigger("DashStop");
            else
                stateMachine.ChangeState(player.crouchState);
        }
        else if (stateDuration < 0f)
        {
            if (horizontalInput == 0)
                rb.velocity = Vector2.zero;
            else if (!player.CheckCeilling())
                stateMachine.ChangeState(player.runState);
            else
                stateMachine.ChangeState(player.crouchState);
        }
        else
        {
            if (!player.CheckSlope())
                rb.velocity = new Vector2(player.dashSpeed * player.facingDir, 0f);
            else
                rb.velocity = new Vector2(player.dashSpeed * player.facingDir * -player.slopeMoveDir.x, player.dashSpeed * player.facingDir * -player.slopeMoveDir.y);
        }
        if (!player.CheckGrounded())
        {
            rb.velocity = Vector2.zero;
            stateMachine.ChangeState(player.fallState);
        }
        if (finishAnim)
            stateMachine.ChangeState(player.idleState);
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.Space))
            stateMachine.ChangeState(player.jumpState);
        if(Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.shieldState);
        if (Input.GetKeyDown(KeyCode.K))
            stateMachine.ChangeState(player.attackState);
    }
}
