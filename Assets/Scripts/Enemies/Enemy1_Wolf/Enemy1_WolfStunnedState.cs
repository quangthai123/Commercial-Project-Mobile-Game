using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfStunnedState : EnemyStates
{
    private Enemy1_Wolf enemy;
    public Enemy1_WolfStunnedState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName, Enemy1_Wolf _enemy) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Start()
    {
        base.Start();
        rb.velocity = new Vector2(0f, rb.velocity.y);
        enemy.entityFx.StartCoroutine(enemy.entityFx.BeCounterAttackedFlashFx());
        enemy.transform.position = new Vector2(enemy.transform.position.x - enemy.facingDir * 1.5f, enemy.transform.position.y);
    }
    public override void Exit()
    {
        base.Exit();
        enemy.canBeStunned = false;
    }
    public override void Update()
    {
        base.Update();
        rb.velocity = new Vector2(0f, rb.velocity.y);
        if (enemy.canBeHitByCounterAttack)
            stateMachine.ChangeState(enemy.hurtState);
    }
}
