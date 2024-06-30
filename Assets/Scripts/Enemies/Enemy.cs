using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine {  get; private set; }

    [Header("Front ground Infor")]
    [SerializeField] private Transform frontGroundCheckPos;
    [SerializeField] private float frontGroundCheckDistance;
    [Header("Detect Player Infor")]
    [SerializeField] private float detectPlayerDistance;
    public bool canBeStunned = false;
    public bool canBeHitByCounterAttack = false;
    public bool isAttacking = false;
    [HideInInspector] public EntityFx entityFx;
    [Header("AI Infor")]
    public float actionMinTime;
    public float actionMaxTime;
    public float attackCooldown;

    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        entityFx = GetComponent<EntityFx>();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    public bool CheckGround() => Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, whatIsGround);
    public bool CheckNotFrontGround() => !Physics2D.Raycast(frontGroundCheckPos.position, Vector2.down, frontGroundCheckDistance, whatIsGround);
    public bool DetectedPlayer1() => Physics2D.Raycast(transform.position, Vector2.right * facingDir, detectPlayerDistance, opponentLayer);
    public bool DetectedPlayer2() => Physics2D.Raycast(transform.position, Vector2.right * -facingDir, detectPlayerDistance, opponentLayer);
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - groundCheckDistance));
        Gizmos.DrawLine(frontGroundCheckPos.position, new Vector2(frontGroundCheckPos.position.x, frontGroundCheckPos.position.y - frontGroundCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x + facingDir * detectPlayerDistance, transform.position.y));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x - facingDir * detectPlayerDistance, transform.position.y));
    }
    public void SetFinishAnim() => stateMachine.currentState.SetFinishAnim();
    public void DoDamagePlayer(int attackWeight)
    {
        Collider2D hit = Physics2D.OverlapCircle(attackPointPos.position, attackRangeRadius, opponentLayer);
        if (hit != null)
        {
            if (hit.GetComponentInParent<Player>() != null)
            {
                hit.GetComponentInParent<Player>().GetDamage(transform, attackWeight);
            }
        }
    }
    public void GetDamage(Transform opponentTransform, int attackWeight)
    {
        entityFx.StartCoroutine(entityFx.FlashFX());
    }
}
