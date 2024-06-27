using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player player;
    //private Transform getDownPos;
    //private Transform oneWayplatform;
    void Start()
    {
        player = Player.Instance;
    }
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetAxisRaw("Vertical") != 0 && player.stateMachine.currentState != player.jumpState)
        {
            if (player.canLadder)
            {
                player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
                player.stateMachine.ChangeState(player.ladderState);
            }
        }
    }
}
