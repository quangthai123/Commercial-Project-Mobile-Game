using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    protected Player player;
    protected Enemy enemyBase;
    protected virtual void Start()
    {
        player = Player.Instance;
        enemyBase = GetComponentInParent<Enemy>();
    }
    protected void SetFinishAnim()
    {
        enemyBase.SetFinishAnim();
    }
    protected void LightAttackTrigger() // Start attacking in anim frame
    {
        enemyBase.isAttacking = true;
        enemyBase.DoDamagePlayer(0);
    }
    protected void NormalAttackTrigger()
    {
        enemyBase.isAttacking = true;
        enemyBase.DoDamagePlayer(1);
    }
    protected void HeavyAttackTrigger()
    {
        enemyBase.isAttacking = true;
        enemyBase.DoDamagePlayer(2);
    }
}
