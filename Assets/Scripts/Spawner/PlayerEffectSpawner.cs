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
    protected override void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        base.Awake();
    }

}
