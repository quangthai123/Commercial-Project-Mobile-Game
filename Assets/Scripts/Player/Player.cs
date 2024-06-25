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
    #endregion

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
    [Header("Shield Infor")]
    public float shieldDuration;
    public bool isShielding = false;
    [Header("Attack Infor")]
    public float allowComboTime;
    [Header("Dash Infor")]
    public float dashSpeed;
    public float dashDuration;
    public float dashCooldown;
    [Header("Ceilling Collision Infor")]
    [SerializeField] private Transform ceillingCheckPos1;
    [SerializeField] private Transform ceillingCheckPos2;
    [SerializeField] private float ceillingCheckDistance;
    //public bool isCeilled;
    [HideInInspector] public GameObject normalCol;
    [HideInInspector] public GameObject dashCol;
    [HideInInspector] public float dashTimer;
    [Header("Landing Infor")]
    public float landingDuration;
    public float allowLandingTime;
    [Header("Slope infor")]
    public bool onSlopeRight = false;
    public Vector2 slopeMoveDir;
    [SerializeField] private LayerMask whatIsSlopeGround;
    [SerializeField] private float slopeCheckDistance;
    private Vector2 slopeNormalAxis;

    private void Awake()
    {
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
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        facingDir = 1;
        dashCol = transform.Find("Dash Col No Trigger").gameObject;
        normalCol = transform.Find("Col No Trigger").gameObject;
    }
    protected override void Update()
    {
        base.Update();
        //isCeilled = CheckCeilling();
        stateMachine.currentState.Update();
        HandleSlopeMoveDir();
    }
    public bool CheckCeilling()
    {
        return Physics2D.Raycast(ceillingCheckPos1.position, Vector2.up, ceillingCheckDistance, whatIsGround) || 
            Physics2D.Raycast(ceillingCheckPos2.position, Vector2.up, ceillingCheckDistance, whatIsGround);
    }
    public bool CheckSlope() => Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, whatIsSlopeGround);
    public override bool CheckGrounded()
    {
        return Physics2D.Raycast(groundCheckPos1.position, Vector2.down, groundCheckDistance, whatIsGround)
            || Physics2D.Raycast(groundCheckPos2.position, Vector2.down, groundCheckDistance, whatIsGround) || CheckSlope();
    }
    private void HandleSlopeMoveDir()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, slopeCheckDistance, whatIsSlopeGround);
        if(hit)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.red);
            Debug.DrawRay(hit.point, Vector2.Perpendicular(hit.normal), Color.green);
            //if(hit.normal.x > 0)
            //{
            //    onSlopeRight = false;
            //    slopeMoveDir = Vector2.Perpendicular(hit.normal).normalized;
            //} else
            //{
            //    onSlopeRight = true;
            //    slopeMoveDir = -Vector2.Perpendicular(hit.normal).normalized;
            //}
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
        Gizmos.DrawLine(ceillingCheckPos1.position, new Vector2(ceillingCheckPos1.position.x, ceillingCheckPos1.position.y + ceillingCheckDistance));
        Gizmos.DrawLine(ceillingCheckPos2.position, new Vector2(ceillingCheckPos2.position.x, ceillingCheckPos2.position.y + ceillingCheckDistance));
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y - slopeCheckDistance));
    }
}
