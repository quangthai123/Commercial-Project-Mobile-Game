using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfAnimationController : EnemyAnimationController
{
    private Enemy1_Wolf enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponentInParent<Enemy1_Wolf>();
    }
    private void JumpUpAttackTrigger()
    {
        if(!enemy.CheckNotFrontGround()) 
            enemy.rb.velocity = new Vector2(enemy.attackForce.x * enemy.facingDir, enemy.attackForce.y);
        else 
            enemy.rb.velocity = new Vector2(0f, enemy.attackForce.y);
    }
}
