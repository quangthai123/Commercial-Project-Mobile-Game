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
        float rdTime = Random.Range(enemy.actionMinTime, enemy.actionMaxTime);
        stateDuration = rdTime;
        rb.velocity = Vector3.zero;
    }
    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if(stateDuration < 0f)
            stateMachine.ChangeState(enemy.runState);
        if (enemy.DetectedPlayer())
            stateMachine.ChangeState(enemy.attackState);
    }
    public override void Exit()
    {
        base.Exit();
    }


}
