using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectSpawner : Spawner
{
    public static PlayerEffectSpawner instance;
    public string dashShadowEffect = "dashShadowFx";
    public string runEffect = "runFx";
    public string startJumpEffect = "startJumpFx";
    public string lightGroundedEffect = "lightGroundedFx";
    public string groundedLeftEffect = "groundedLeftFx";
    public string groundedRightEffect = "groundedRightFx";
    public string startDashEffect = "startDashFx";
    public string endDashEffect = "endDashFx";
    public string doubleJumpEffect = "doubleJumpFx";
    public string airDashEffect = "airDashFx";
    public string parryEffect = "parryEffect";
    public string strongGroundedEffect = "strongGroundedFx";
    public string healEffect = "healFx";
    public string flashParryEffect = "flashParryFx";
    public string flashParryEffect2 = "flashParryFx2";
    public string strongParryEffect = "strongParryFx";
    public string attackImpactEffect = "attackImpactFx";
    public string attackImpact2Effect = "attackImpactFx2";
    public string attackImpact3Effect = "attackImpactFx3";
    public string attackImpact4Effect = "attackImpactFx4";
    public string hitImpactEffect = "hitFx";
    public string hitGroundedEffect = "hitGroundedFx";
    protected override void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        base.Awake();
    }
    public override Transform Spawn(string name, Vector2 pos, Quaternion rot)
    {
        base.Spawn(name, pos, rot);
        if(obj.GetComponent<PlayerEffectDespawnByAnim>() != null)
        {
            if(obj.GetComponent<PlayerEffectDespawnByAnim>().canFlip && obj.GetComponent<PlayerEffectDespawnByAnim>().wrongDirWhenSpawn && Player.Instance.facingDir == 1)
                obj.GetComponent<PlayerEffectDespawnByAnim>().Flip();
            else if(obj.GetComponent<PlayerEffectDespawnByAnim>().canFlip && !obj.GetComponent<PlayerEffectDespawnByAnim>().wrongDirWhenSpawn && Player.Instance.facingDir == -1)
                obj.GetComponent<PlayerEffectDespawnByAnim>().Flip();
        }
        obj.gameObject.SetActive(true);
        return obj;
    }
}
