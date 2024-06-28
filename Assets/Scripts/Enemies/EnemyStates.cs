using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates
{
    private string animBoolName;
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected Rigidbody2D rb;
    protected float stateDuration;
    protected bool finishAnim = false;
    public EnemyStates(Enemy _enemyBase, EnemyStateMachine _enemyStateMachine, string _animBoolName)
    {
        this.enemyBase = _enemyBase;
        this.stateMachine = _enemyStateMachine;
        this.animBoolName = _animBoolName;
    }

    public virtual void Start()
    {
        enemyBase.anim.SetBool(animBoolName, true);
        rb = enemyBase.rb;
    }

    public virtual void Update()
    {
        stateDuration -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemyBase.anim.SetBool(animBoolName, false);
        finishAnim = false;
    }
    public void SetFinishAnim()
    {
        finishAnim = true;
    }
}
