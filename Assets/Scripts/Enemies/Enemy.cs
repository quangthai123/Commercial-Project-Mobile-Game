using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine {  get; private set; }
    [SerializeField] private Transform frontGroundCheckPos;
    [Header("Detect Player Infor")]
    [SerializeField] private float detectPlayerDistance;
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public bool CheckGround() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool CheckNotFrontGround() => !Physics2D.Raycast(frontGroundCheckPos.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool DetectedPlayer() => Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectPlayerDistance, opponentLayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(frontGroundCheckPos.position, new Vector2(frontGroundCheckPos.position.x, frontGroundCheckPos.position.y - groundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + facingDir * detectPlayerDistance, transform.position.y));
    }
}
