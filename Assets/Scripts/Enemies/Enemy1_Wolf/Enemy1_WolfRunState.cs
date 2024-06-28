using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class Enemy1_WolfRunState : EnemyStates
{
    private Enemy1_Wolf enemy;
    public Enemy1_WolfRunState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        float rdTime = Random.Range(enemy.actionMinTime, enemy.actionMaxTime);
        stateDuration = rdTime;
        int rdDir = Random.Range(0, 2);
        if (rdDir == 1)
            enemy.Flip();
    }
    public override void Exit()
    {
        base.Exit();
        rb.velocity = Vector3.zero;
    }


    public override void Update()
    {
        base.Update();
        if(!enemy.DetectedPlayer())
        {
            rb.velocity = new Vector2(enemy.moveSpeed * enemy.facingDir, 0f);
            if (stateDuration < 0f)
                stateMachine.ChangeState(enemy.idleState);
        } else 
            stateMachine.ChangeState(enemy.attackState);
        if (enemy.CheckNotFrontGround() || enemy.CheckWalled())
        {
            rb.velocity = Vector2.zero;
            enemy.Flip();
        }
    }
}
