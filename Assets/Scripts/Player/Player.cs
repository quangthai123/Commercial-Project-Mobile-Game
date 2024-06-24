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
    [HideInInspector] public GameObject normalCol;
    [HideInInspector] public GameObject dashCol;
    [HideInInspector] public float dashTimer; 
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
        stateMachine.currentState.Update();
    }
    public void SetFinishCurrentAnimation()
    {
        stateMachine.currentState.SetFinishAnimation();
    }
}
