using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfAttackState : EnemyStates
{
    private Enemy1_Wolf enemy;
    //private bool attacked = false;
    public Enemy1_WolfAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = Vector3.zero;
        //attacked = false;
    }
    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector3.zero;
        enemy.isAttacking = false;
        enemy.transform.Find("Col Trigger").gameObject.layer = LayerMask.NameToLayer("CanDamagePlayer");
    }


    public override void Update()
    {
        base.Update();
        //if (enemy.CheckOpponentInAttackRange())
        //    rb.velocity = Vector3.zero;
        //else
        //{
        //    if(!enemy.CheckNotFrontGround() && !attacked)
        //    {
        //        rb.velocity = new Vector2(enemy.attackForce.x * enemy.facingDir, enemy.attackForce.y);
        //        attacked = true;
        //    }
        //    else if(enemy.CheckNotFrontGround() && !attacked)
        //    {
        //        rb.velocity = new Vector2(0f, enemy.attackForce.y);
        //        attacked = true;
        //    }
        //}
        if (enemy.CheckNotFrontGround())
            rb.velocity = new Vector2(0f, rb.velocity.y);
        if (enemy.canBeStunned)
            stateMachine.ChangeState(enemy.stunnedState);
        if (enemy.CheckGround() && rb.velocity.y < -.1f)
            rb.velocity = new Vector2(0f, rb.velocity.y);
        if (finishAnim && enemy.CheckGround())
        {
            Debug.Log("Stop Attack");
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
