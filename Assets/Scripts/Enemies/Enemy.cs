using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    public EnemyStateMachine stateMachine {  get; private set; }

    [Header("Front ground Infor")]
    [SerializeField] protected Transform frontGroundCheckPos;
    [SerializeField] protected float frontGroundCheckDistance;
    [Header("Detect Player Infor")]
    [SerializeField] protected float detectPlayerDistance;
    public bool canBeStunned = false;
    public bool canBeHitByCounterAttack = false;
    public bool isAttacking = false;
    [HideInInspector] public EntityFx entityFx;
    [Header("AI Infor")]
    public float actionMinTime;
    public float actionMaxTime;
    public float attackCooldown;
    protected Player player;
    [HideInInspector] public EnemyStats enemyStats;
    protected virtual void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }
    protected override void Start()
    {
        base.Start();
        entityFx = GetComponent<EntityFx>();
        enemyStats = GetComponent<EnemyStats>();
        player = Player.Instance;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        isDead = enemyStats.currentHealth == 0;
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
                hit.GetComponentInParent<Player>().GetDamage(transform, attackWeight, false);
            }
        }
    }
    public void GetDamage(Transform opponentTransform, int attackWeight, float _damage)
    {
        entityFx.StartCoroutine(entityFx.FlashFX());
        float rdX = Random.Range(0f, 1f);
        float rdY = Random.Range(-1f, 1f);
        Vector2 rdPos = new Vector2(player.attackEffectPos.position.x + rdX, player.attackEffectPos.position.y + rdY);
        switch(attackWeight)
        {
            case 0:
                PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.attackImpactEffect, rdPos, Quaternion.identity); break;
            case 1:
                PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.attackImpact2Effect, rdPos, Quaternion.identity); break;
            case 2:
                PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.attackImpact3Effect, new Vector2(player.attackEffectPos.position.x, player.attackEffectPos.position.y + .33f), Quaternion.identity); break;
            case 3:
                PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.attackImpact4Effect, rdPos, Quaternion.identity); break;
        }
        enemyStats.GetDamageStat(_damage);
    }
}
