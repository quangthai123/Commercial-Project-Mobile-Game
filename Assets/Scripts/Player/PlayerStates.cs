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
        rb.gravityScale = 6f;
        player.knockFlip = false;
    }
    public virtual void Update()
    {
        ChangeStateByInput();
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        stateDuration -= Time.deltaTime;
        if (player.CheckSlope())
        {
            //rb.velocity = new Vector2(0f, rb.velocity.y);
            //rb.gravityScale = 0f;
            if(player.stateMachine.currentState != player.runState)
            {
                rb.sharedMaterial = player.onSlopePhysicMat;
            }
            MakeEnableSlopeCol(true);
        }
        else
        {
            rb.sharedMaterial = player.normalPhysicMat;
            MakeEnableSlopeCol(false);
        }
    }

    public virtual void Exit()
    {
        player.anim.SetBool(animBoolName, false);
        finishAnim = false;
        rb.gravityScale = 6f;
        player.knockFlip = false;
    }
    public void SetFinishAnimation()
    {
        finishAnim = true;
    }
    protected virtual void ChangeStateByInput()
    {

    }
    protected void MakeEnableSlopeCol(bool _enabled)
    {
        player.normalCol.GetComponent<CapsuleCollider2D>().enabled = _enabled; // => slope col
        player.normalCol.GetComponent<BoxCollider2D>().enabled = !_enabled;
    }
}
