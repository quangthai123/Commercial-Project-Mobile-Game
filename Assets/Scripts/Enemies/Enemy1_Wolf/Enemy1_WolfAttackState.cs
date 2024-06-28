using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfAttackState : EnemyStates
{
    private Enemy1_Wolf enemy;
    private float rageTime;
    public Enemy1_WolfAttackState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        rageTime = enemy.rageDuration;
        enemy.anim.speed = 2f;
    }
    public override void Exit()
    {
        base.Exit();
        enemy.anim.speed = 1f;
        rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        if (!enemy.DetectedPlayer())
            rageTime -= Time.deltaTime;
        else
            rageTime = enemy.rageDuration;
        rb.velocity = new Vector2(enemy.rageSpeed * enemy.facingDir, 0f);
        if (enemy.CheckNotFrontGround() || enemy.CheckWalled())
        {
            rb.velocity = Vector2.zero;
            enemy.Flip();
        }
        if (rageTime < 0f)
            stateMachine.ChangeState(enemy.idleState);
    }
}
