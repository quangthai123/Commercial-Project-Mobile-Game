using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Animator anim { get; protected set; }
    public Rigidbody2D rb { get; protected set; }
    
    public float moveSpeed;
    public int facingDir;
    [Header("Ground collision Infor")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    
    [Header("Wall collision Infor")]
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsWall;
    [SerializeField] protected Transform wallCheckPos;
    [Header("Check Opponent Infor")]
    [SerializeField] protected Transform attackPointPos;
    [SerializeField] protected float attackRangeRadius;
    [SerializeField] protected LayerMask opponentLayer;
    [HideInInspector] public bool knockFlip;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        FlipController();
    }
    protected virtual void FlipController()
    {
        if (rb.velocity.x < -.1f && facingDir == 1)
            Flip();
        if (rb.velocity.x > .1f && facingDir == -1)
            Flip();
    }
    public void Flip()
    {
        if(knockFlip) return;
        transform.Rotate(0f, 180f, 0f);
        facingDir *= -1;
    }
    public virtual bool CheckWalled() => Physics2D.Raycast(wallCheckPos.position, Vector2.right * facingDir, wallCheckDistance, whatIsWall);
    public bool CheckOpponentInAttackRange() => Physics2D.OverlapCircle(attackPointPos.position, attackRangeRadius, opponentLayer);
    protected virtual void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(attackPointPos.position, attackRangeRadius);
        Gizmos.DrawLine(wallCheckPos.position, new Vector2(wallCheckPos.position.x + facingDir * wallCheckDistance, wallCheckPos.position.y));
    }
}
