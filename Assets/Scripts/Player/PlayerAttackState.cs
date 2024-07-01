using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStates
{
    public int comboCounter = 0;
    private float attackExitTime;
    public int airAttackCounter = 1;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
        if (player.CheckGrounded())
        {
            if (Time.time - attackExitTime > player.allowComboTime || !player.CheckOpponentInAttackRange())
                comboCounter = 0;
        }
        else if (airAttackCounter == 1)
        {
            player.anim.SetBool("AirAttack1", true);
            return;
        }
        else if (airAttackCounter == 2)
        {
            player.anim.SetBool("AirAttack2", true);
            return;
        }
        else
        {
            stateMachine.ChangeState(player.fallState);
            return;
        }
        player.anim.SetInteger("AttackCount", comboCounter);
        
    }
    public override void Exit()
    {
        base.Exit();
        player.anim.SetBool("AirAttack1", false);
        player.anim.SetBool("AirAttack2", false);
        if (player.CheckGrounded())
        {
            attackExitTime = Time.time;
            if (comboCounter < 3 && player.CheckOpponentInAttackRange())
                comboCounter++;
            else
                comboCounter = 0;
        }
        else
            airAttackCounter++;
    }


    public override void Update()
    {
        base.Update();
        if (player.CheckGrounded())
            rb.velocity = Vector2.zero;
        else
            rb.velocity = new Vector2(horizontalInput * 0.8f * player.moveSpeed, rb.velocity.y);
        if (finishAnim && player.CheckGrounded())
            stateMachine.ChangeState(player.idleState);
        else if (finishAnim && !player.CheckGrounded())
            stateMachine.ChangeState(player.fallState);
        if (horizontalInput != 0 && horizontalInput != player.facingDir)
            player.Flip();
    }

    protected override void ChangeStateByInput()
    {
        base.ChangeStateByInput();
        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(player.dashState);
        if (Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.shieldState);
    }
}
