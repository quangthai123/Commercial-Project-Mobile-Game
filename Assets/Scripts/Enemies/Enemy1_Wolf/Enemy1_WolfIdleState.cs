using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfIdleState : EnemyStates
{
    private Enemy1_Wolf enemy;
    public Enemy1_WolfIdleState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Start()
    {
        base.Start();
        if (!enemy.DetectedPlayer())
        {
            float rdTime = Random.Range(enemy.actionMinTime, enemy.actionMaxTime);
            stateDuration = rdTime;
        }
        else if (enemy.CheckOpponentInAttackRange())
            stateDuration = enemy.attackCooldown;
        rb.velocity = Vector3.zero;
    }
    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (!enemy.CheckOpponentInAttackRange())
        {
            if (stateDuration < 0f || enemy.DetectedPlayer())
                stateMachine.ChangeState(enemy.runState);
        }
        else if(stateDuration < 0f)
            stateMachine.ChangeState(enemy.attackState);

    }
    public override void Exit()
    {
        base.Exit();
    }


}
