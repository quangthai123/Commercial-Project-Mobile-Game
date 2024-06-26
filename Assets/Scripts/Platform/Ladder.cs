using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = Player.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetAxisRaw("Vertical") != 0)
        {
            if (player.canLadder)
            {
                player.transform.position = new Vector2(transform.position.x, player.transform.position.y);
                player.stateMachine.ChangeState(player.ladderState);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetAxisRaw("Vertical") != 0)
        {
            player.stateMachine.ChangeState(player.idleState);
        }
    }
}
