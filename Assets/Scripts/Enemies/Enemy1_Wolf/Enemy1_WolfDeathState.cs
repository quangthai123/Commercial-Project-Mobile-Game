using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_WolfDeathState : EnemyStates
{
    public Enemy1_WolfDeathState(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName) : base(_enemyBase, _enemyStateMachine, _animBoolName)
    {
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Exit()
    {
        base.Exit();
    }


    public override void Update()
    {
        base.Update();
    }
}
