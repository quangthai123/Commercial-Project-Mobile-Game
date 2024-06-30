using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfHurtState : EnemyStates
{
    private Enemy1_Wolf enemy;

    public Enemy1_WolfHurtState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = Vector3.zero;
        Debug.Log("Counter Attacked!!!");
    }
    public override void Exit()
    {
        base.Exit();
        enemy.canBeHitByCounterAttack = false;
    }


    public override void Update()
    {
        base.Update();
        rb.velocity = Vector3.zero;
        if (finishAnim)
            stateMachine.ChangeState(enemy.idleState);
    }
}
