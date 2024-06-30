using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1_Wolf : Enemy
{
    public Enemy1_WolfIdleState idleState {  get; private set; }
    public Enemy1_WolfRunState runState { get; private set; }
    public Enemy1_WolfHurtState hurtState { get; private set; }
    public Enemy1_WolfStunnedState stunnedState { get; private set; }
    public Enemy1_WolfAttackState attackState { get; private set; }
    [Header("Attack Force")]
    public Vector2 attackForce;
    protected override void Awake()
    {
        base.Awake();
        idleState = new Enemy1_WolfIdleState(this, stateMachine, "Idle", this);
        runState = new Enemy1_WolfRunState(this, stateMachine, "Run", this);
        hurtState = new Enemy1_WolfHurtState(this, stateMachine, "Hit", this);
        stunnedState = new Enemy1_WolfStunnedState(this, stateMachine, "Stunned", this);
        attackState = new Enemy1_WolfAttackState(this, stateMachine, "Attack", this);
    }
    public bool DetectedPlayer() => DetectedPlayer1() || DetectedPlayer2();
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        facingDir = -1;
    }

}
