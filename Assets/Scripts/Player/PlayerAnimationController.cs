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
}
