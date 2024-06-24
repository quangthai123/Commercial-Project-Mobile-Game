using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStates
{
    protected string animBoolName;
    protected Player player;
    protected PlayerStateMachine stateMachine;
    protected Rigidbody2D rb;
    protected bool finishAnim;
    protected float horizontalInput;
    protected float verticalInput;
    protected float stateDuration;
    public PlayerStates(Player _player, PlayerStateMachine _stateMachine, string _animBoolName)
    {
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = _animBoolName;
    }
    public virtual void Start()
    {
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }
    public virtual void Update()
    {
        ChangeStateByInput();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        stateDuration -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        finishAnim = false;
    }
    public void SetFinishAnimation()
    {
        finishAnim = true;
    }
    protected virtual void ChangeStateByInput()
    {

    }
}
