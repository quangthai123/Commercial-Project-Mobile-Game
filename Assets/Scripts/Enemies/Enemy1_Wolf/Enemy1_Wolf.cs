using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Wolf : Enemy
{
    public Enemy1_WolfIdleState idleState {  get; private set; }
    public Enemy1_WolfRunState runState { get; private set; }
    public Enemy1_WolfAttackState attackState { get; private set; }
    [Header("AI Infor")]
    public float actionMinTime;
    public float actionMaxTime;
    public float rageDuration;
    public float rageSpeed; 
    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy1_WolfIdleState(this, stateMachine, "Idle", this);
        runState = new Enemy1_WolfRunState(this, stateMachine, "Run", this);
        attackState = new Enemy1_WolfAttackState(this, stateMachine, "Run", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        facingDir = -1;
    }

}
