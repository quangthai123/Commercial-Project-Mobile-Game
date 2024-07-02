using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Player player;
    public Transform currentEnemyTarget;
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetFinishAnimation()
    {
        Player.Instance.SetFinishCurrentAnimation();
    }
    private void SetPlayerIsBlockingAfterFinsishShield()
    {
        Player.Instance.isShielding = true;
    }
    private void SetPlayerIsNotBlockingAfterFinsishShield()
    {
        Player.Instance.isShielding = false;
    }
    private void FirstMovePlayerWhileLedgeClimb()
    {
        Player.Instance.transform.position = new Vector2(Player.Instance.transform.position.x - Player.Instance.facingDir * 0.2f, Player.Instance.transform.position.y + 0.5f);
    }
    private void SecondMovePlayerWhileLedgeClimb()
    {
        Player.Instance.transform.position = new Vector2(Player.Instance.transform.position.x + Player.Instance.facingDir * 0.2f, Player.Instance.transform.position.y + 0.6f);
    }
    private void ThirdMovePlayerWhileLedgeClimb()
    {
        Player.Instance.transform.position = new Vector2(Player.Instance.transform.position.x + Player.Instance.facingDir * 0.6f, Player.Instance.transform.position.y + 0.7f);
    }
    private void LastMovePlayerWhileLedgeClimb()
    {
        Player.Instance.transform.position = new Vector2(Player.Instance.transform.position.x + Player.Instance.facingDir * 1.75f, Player.Instance.transform.position.y + 1.8f);
    }
    private void SpawnRunEffect()
    {
        if(!player.CheckSlope())
            PlayerEffectSpawner.instance.Spawn("runFx", Player.Instance.leftEffectPos.position, Quaternion.identity);
    }
    private void StartHealEffect()
    {
        PlayerEffectSpawner.instance.Spawn(PlayerEffectSpawner.instance.healEffect, Player.Instance.centerEffectPos.position, Quaternion.identity);
    }
    public void CounterAttackCurrentEnemyTarget()
    {
        if(currentEnemyTarget != null)
        {
            currentEnemyTarget.GetComponentInParent<Enemy>().canBeHitByCounterAttack = true;
        }
        currentEnemyTarget = null;
    }
    public void AttackTrigger(int attackWeight)
    {
        switch (attackWeight)
        {
            case 0:
                player.DoDamageEnemy(attackWeight, player.playerStats.damage.GetValue()); break;
            case 1:
                player.DoDamageEnemy(attackWeight, player.playerStats.damage.GetValue() * 1.1f); break;
            case 2:
                player.DoDamageEnemy(attackWeight, player.playerStats.damage.GetValue() * 1.5f); break;
            case 3:
                player.DoDamageEnemy(attackWeight, player.playerStats.damage.GetValue() * 1.25f); break;
            case 4:
                player.DoDamageEnemy(attackWeight-1, player.playerStats.damage.GetValue() * 2.5f); break;
        }
    }
}
