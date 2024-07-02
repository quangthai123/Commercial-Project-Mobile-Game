using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static Player Instance { get; private set; }
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerRunState runState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerFallState fallState { get; private set; }
    public PlayerShieldState shieldState { get; private set; }
    public PlayerAttackState attackState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerAirDashState airDashState { get; private set; }
    public PlayerHealState healState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerWallJumpState wallJumpState { get; private set; }
    public PlayerCrouchState crouchState { get; private set; }
    public PlayerEnterCrouchState enterCrouchState { get; private set; }
    public PlayerExitCrouchState exitCrouchState { get; private set; }
    public PlayerLandingState landingState { get; private set; }
    public PlayerLadderState ladderState { get; private set; }
    public PlayerLedgeGrabState ledgeGrabState { get; private set; }
    public PlayerLedgeClimbState ledgeClimbState { get; private set; }
    public PlayerHurtState hurtState { get; private set; }
    public PlayerStrongHurtState strongHurtState { get; private set; }
    public PlayerKnockoutState knockoutState { get; private set; }
    public PlayerParryState parryState { get; private set; }
    public PlayerCounterAttackState counterAttackState { get; private set; }
    public PlayerDeathState deathState { get; private set; }
    #endregion
    [SerializeField] protected Transform groundCheckPos1;
    [SerializeField] protected Transform groundCheckPos2;
    [Header("Dash Infor")]
    public float dashSpeed;
    public float dashDuration; // original 0.3f // update 0.45f
    public float dashCooldown; // original 0.8f // update 0.4f
    [Header("Shield Infor")]
    public float shieldDuration; // original 0.2f // update 0.5f
    public bool isShielding = false;
    [Header("Heal Infor")]
    public float healingDuration; // original 1.8f // update 0.9f
    public bool cantBeHurtWhileHealing = false;
    [Space]
    [Header("Jump Infor")]
    public float jumpDuration;
    public float jumpForce;
    public float jumpMinTime;
    [Header("Wall Jump Infor")]
    public Vector2 wallJumpForce;
    public GameObject wallSlideCol;
    public float allowWallJumpUpMaxTime;
    public float allowWallJumpUpMinTime;
    [HideInInspector] public bool doubleJumped;
    [Header("Attack Infor")]
    public float allowComboTime;
    [SerializeField] private float spawnDashShadowCooldown;
    [SerializeField] private bool startDashShadowCoroutine = false;
    [Header("Ceilling Collision Infor")]
    [SerializeField] private Transform ceillingCheckPos1;
    [SerializeField] private Transform ceillingCheckPos2;
    [SerializeField] private float ceillingCheckDistance;
    [HideInInspector] public GameObject normalCol;
    [HideInInspector] public GameObject dashCol;
    [HideInInspector] public float dashTimer;
    [Header("Landing Infor")]
    public float landingDuration;
    public float allowLandingTime;
    [Header("Slope infor")]
    public PhysicsMaterial2D normalPhysicMat;
    public PhysicsMaterial2D onSlopePhysicMat;
    public bool onSlope = false;
    public Vector2 slopeMoveDir;
    [SerializeField] private float slopeAngleWithUpAxis;
    [SerializeField] private LayerMask whatIsSlopeGround;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private float jumpOnSlopeCheckDistance;
    private Vector2 slopeNormalAxis;
    [Header("Ladder Infor")]
    public bool canLadder = false;
    public float ladderMoveSpeed;
    [Header("Ledge Infor")]
    public Transform ledgeCheckPos;
    public Transform wallCheckPosForEdge;
    [SerializeField] private float ledgeCheckRadius;
    [HideInInspector] public bool canGrabLedge = true;
    private Coroutine dashShadowCoroutine;
    [Header("Effect Infor")]
    public Transform leftEffectPos;
    public Transform rightEffectPos;
    public Transform centerEffectPos;
    public Transform shieldEffectPos;
    public Transform attackEffectPos;
    public Transform hitEffectPos;
    [Header("Knockback")]
    public Vector2 currentKnockbackDir;
    [SerializeField] protected float currentKnockbackDuration;
    [SerializeField] protected List<Vector2> knockbackDirs;
    [SerializeField] protected List<float> knockbackDurations;
    public bool isKnocked;
    public float knockOutDuration;
    private EntityFx entityFx;

    [Header("Parry Infor")]
    public float parryDuration;
    public float strongParryDuration;
    public float pushBackSpeed;
    [HideInInspector] public bool isStrongStrike;
    public float spawnParryEffectCooldown;
    [HideInInspector] public PlayerStats playerStats;
    private void Awake()
    {
        //Time.timeScale = 0.1f;
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        runState = new PlayerRunState(this, stateMachine, "Run");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        fallState = new PlayerFallState(this, stateMachine, "Fall");
        shieldState = new PlayerShieldState(this, stateMachine, "Shield");
        attackState = new PlayerAttackState(this, stateMachine, "IsAttacking");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        airDashState = new PlayerAirDashState(this, stateMachine, "AirDash");
        healState = new PlayerHealState(this, stateMachine, "Heal");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "Walled");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "Jump");
        crouchState = new PlayerCrouchState(this, stateMachine, "Crouch");
        enterCrouchState = new PlayerEnterCrouchState(this, stateMachine, "EnterCrouch");
        exitCrouchState = new PlayerExitCrouchState(this, stateMachine, "ExitCrouch");
        landingState = new PlayerLandingState(this, stateMachine, "Landing");
        ladderState = new PlayerLadderState(this, stateMachine, "Ladder");
        ledgeGrabState = new PlayerLedgeGrabState(this, stateMachine, "LedgeGrab");
        ledgeClimbState = new PlayerLedgeClimbState(this, stateMachine, "LedgeClimb");
        hurtState = new PlayerHurtState(this, stateMachine, "Hurt");
        strongHurtState = new PlayerStrongHurtState(this, stateMachine, "StrongHurt");
        knockoutState = new PlayerKnockoutState(this, stateMachine, "Knockout");
        parryState = new PlayerParryState(this, stateMachine, "Parry");
        counterAttackState = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");
        deathState = new PlayerDeathState(this, stateMachine, "Dead");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        facingDir = 1;
        dashCol = transform.Find("Dash Col No Trigger").gameObject;
        normalCol = transform.Find("Col No Trigger").gameObject;
        entityFx = GetComponent<EntityFx>();
        playerStats = GetComponent<PlayerStats>();
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        HandleSlopeMoveDir();
        onSlope = CheckSlope();
        isDead = playerStats.currentHealth == 0;
        if ((stateMachine.currentState == dashState || stateMachine.currentState == airDashState) && !startDashShadowCoroutine)
        {
            Debug.Log("Start Coroutine");
            startDashShadowCoroutine = true;
            dashShadowCoroutine = StartCoroutine(SpawnDashShadow());
        }
        else if (stateMachine.currentState != dashState && stateMachine.currentState != airDashState && dashShadowCoroutine != null)
        {
            Debug.Log("Stop Coroutine");
            startDashShadowCoroutine = false;
            StopCoroutine(dashShadowCoroutine);
        }
        //Test 
        //if(Input.GetKeyDown(KeyCode.Alpha1))
        //    GetDamage(groundCheckPos2, 0);
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    GetDamage(groundCheckPos2, 1);
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    GetDamage(groundCheckPos2, 2);
        //}
    }
    protected override void FlipController()
    {
        if (isKnocked)
            return;
        //if(!CheckSlope()) 
        //    base.FlipController();
        if(!CheckGrounded())
            base.FlipController();
        else
        {
            if (Input.GetAxisRaw("Horizontal") < 0 && facingDir == 1)
                Flip();
            if (Input.GetAxisRaw("Horizontal") > 0 && facingDir == -1)
                Flip();
        }

    }
    private IEnumerator SpawnDashShadow()
    {
        while(startDashShadowCoroutine)
        {
            PlayerEffectSpawner.instance.Spawn("dashShadowFx", transform.position, Quaternion.identity);
            //newShadow.gameObject.SetActive(true);
            //if (newShadow == null)
            //    Debug.Log("shadow null");
            yield return new WaitForSeconds(spawnDashShadowCooldown);
        }
    }
    public bool CheckLedge() => Physics2D.OverlapCircle(ledgeCheckPos.position, ledgeCheckRadius, whatIsGround) && !CheckLedgeGround();
    public bool CheckLedgeGround() => Physics2D.Raycast(wallCheckPosForEdge.position, Vector2.right * facingDir, wallCheckDistance + 1f, whatIsGround);
    public bool CheckCeilling()
    {
        return Physics2D.Raycast(ceillingCheckPos1.position, Vector2.up, ceillingCheckDistance, whatIsGround) || 
            Physics2D.Raycast(ceillingCheckPos2.position, Vector2.up, ceillingCheckDistance, whatIsGround);
    }
    public bool CheckSlope()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, whatIsSlopeGround) && slopeAngleWithUpAxis < 30f;
    }
    public bool CheckGrounded()
    {
        return Physics2D.Raycast(groundCheckPos1.position, Vector2.down, groundCheckDistance, whatIsGround)
            || Physics2D.Raycast(groundCheckPos2.position, Vector2.down, groundCheckDistance, whatIsGround) || CheckSlope();
    }
    public bool CheckGroundedWhileHurtOrParry() => Physics2D.Raycast(new Vector2(groundCheckPos1.position.x - facingDir * .5f, groundCheckPos1.position.y), Vector2.down, slopeCheckDistance + 2f, whatIsGround);
    public bool CheckJumpOnSlope() => Physics2D.Raycast(groundCheckPos1.position, Vector2.down, jumpOnSlopeCheckDistance, whatIsSlopeGround)
            || Physics2D.Raycast(groundCheckPos2.position, Vector2.down, jumpOnSlopeCheckDistance, whatIsSlopeGround);
    private void HandleSlopeMoveDir()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, whatIsSlopeGround);
        if(hit)
        {
            slopeAngleWithUpAxis = Vector2.Angle(Vector2.up, hit.normal);
            Debug.DrawRay(hit.point, hit.normal, Color.red);
            Debug.DrawRay(hit.point, Vector2.Perpendicular(hit.normal), Color.green);
            slopeMoveDir = Vector2.Perpendicular(hit.normal).normalized;
        }
    }
    public void SetFinishCurrentAnimation()
    {
        stateMachine.currentState.SetFinishAnimation();
    }
    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(groundCheckPos1.position, new Vector2(groundCheckPos1.position.x, groundCheckPos1.position.y - jumpOnSlopeCheckDistance));
        Gizmos.DrawLine(groundCheckPos2.position, new Vector2(groundCheckPos2.position.x, groundCheckPos2.position.y - jumpOnSlopeCheckDistance));
        Gizmos.DrawLine(ceillingCheckPos1.position, new Vector2(ceillingCheckPos1.position.x, ceillingCheckPos1.position.y + ceillingCheckDistance));
        Gizmos.DrawLine(ceillingCheckPos2.position, new Vector2(ceillingCheckPos2.position.x, ceillingCheckPos2.position.y + ceillingCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - slopeCheckDistance));
        Gizmos.DrawWireSphere(ledgeCheckPos.position, ledgeCheckRadius);
        Gizmos.DrawLine(wallCheckPosForEdge.position, new Vector2(wallCheckPosForEdge.position.x + facingDir * (wallCheckDistance + 1f), wallCheckPosForEdge.position.y));
        Gizmos.DrawLine(groundCheckPos1.position, new Vector2(groundCheckPos1.position.x, groundCheckPos1.position.y - groundCheckDistance));
        Gizmos.DrawLine(groundCheckPos2.position, new Vector2(groundCheckPos2.position.x, groundCheckPos2.position.y - groundCheckDistance));
    }
    public void DoDamageEnemy(int attackWeight, float _damage)
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPointPos.position, attackRangeRadius, opponentLayer);
        foreach (Collider2D hit in hits)
        {
            if(hit.GetComponentInParent<Enemy>() != null)
            {
                hit.GetComponentInParent<Enemy>().GetDamage(transform, attackWeight, _damage);
            }
        }
    }
    public virtual void GetDamage(Transform opponentTransform, int attackWeight, bool isImpactDamage)
    {
        if (isKnocked || cantBeHurtWhileHealing)
            return;
        isKnocked = true;
        if(isShielding && ((opponentTransform.position.x < transform.position.x && facingDir == -1) || 
            (opponentTransform.position.x > transform.position.x && facingDir == 1)))
        {
            if(attackWeight == 0 && opponentTransform.GetComponentInParent<Enemy>().isAttacking)
            {
                stateMachine.ChangeState(counterAttackState);
                opponentTransform.GetComponentInParent<Enemy>().canBeStunned = true;
                anim.GetComponent<PlayerAnimationController>().currentEnemyTarget = opponentTransform;
                return;
            }
            entityFx.StartCoroutine("FlashFX");
            if(attackWeight == 1)
            {
                currentKnockbackDir = knockbackDirs[3];
                currentKnockbackDuration = knockbackDurations[3];
                isStrongStrike = false;
            } else if(attackWeight == 2)
            {
                currentKnockbackDir = knockbackDirs[4];
                currentKnockbackDuration = knockbackDurations[4];
                isStrongStrike = true;
            }
            if(CheckGroundedWhileHurtOrParry())
                rb.velocity = new Vector2(currentKnockbackDir.x * -facingDir, currentKnockbackDir.y);
            stateMachine.ChangeState(parryState);
            return;
        }
        entityFx.StartCoroutine("FlashFX");
        HitKnockback(opponentTransform, attackWeight);
        if (!isImpactDamage)
            playerStats.GetDamageStat(opponentTransform.GetComponentInParent<Enemy>().enemyStats.damage);
        else
            playerStats.GetDamageStat(opponentTransform.GetComponentInParent<Enemy>().enemyStats.impactDamage);
        Debug.Log(gameObject.name + " was damaged!");
        if (!CheckGrounded() && attackWeight == 0)
        {
            isKnocked = false;
            return;
        }
        if(attackWeight==0)
            stateMachine.ChangeState(hurtState);
        else
            stateMachine.ChangeState(strongHurtState);
    }
    protected virtual void HitKnockback(Transform opponentTransform, int attackWeight)
    {
        if ((opponentTransform.position.x > transform.position.x && facingDir == -1) || (opponentTransform.position.x < transform.position.x && facingDir == 1))
            Flip();
        StartCoroutine(Knockback(attackWeight));
    }
    protected virtual IEnumerator Knockback(int attackWeight)
    {
        if (attackWeight == 0)
        {
            if (!CheckGrounded())
                yield break;
            currentKnockbackDir = knockbackDirs[0];
            currentKnockbackDuration = knockbackDurations[0];
        }
        else if (attackWeight == 1)
        {
            currentKnockbackDir = knockbackDirs[1];
            currentKnockbackDuration = knockbackDurations[1];
        }
        else
        {
            currentKnockbackDir = knockbackDirs[2];
            currentKnockbackDuration = knockbackDurations[2];
        }
        if (attackWeight == 0 && !CheckGroundedWhileHurtOrParry())
            rb.velocity = new Vector2(0f, currentKnockbackDir.y);
        else 
            rb.velocity = new Vector2(currentKnockbackDir.x * -facingDir, currentKnockbackDir.y);
        yield return new WaitForSeconds(currentKnockbackDuration);
        if (attackWeight == 0)
            rb.velocity = new Vector2(rb.velocity.x, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canLadder = true;
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("CanDamagePlayer"))
        {
            if(stateMachine.currentState != dashState && stateMachine.currentState != airDashState)
            {
                GetDamage(collision.transform, 0, true);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canLadder = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            canLadder = false;
        }
    }
}
