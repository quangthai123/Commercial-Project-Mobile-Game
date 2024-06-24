using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
}
