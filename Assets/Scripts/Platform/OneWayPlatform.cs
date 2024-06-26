using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    private BoxCollider2D boxCol;
    private Transform player;
    [SerializeField] private bool canJumpDown = false;

    void Start()
    {
        boxCol = GetComponent<BoxCollider2D>();
        player = Player.Instance.transform;
    }
    private void Update()
    {
        if (player.position.y >= transform.position.y + 2.1f && !canJumpDown)
        {
            boxCol.enabled = true;
        } else if(player.position.y < transform.position.y + 2.1f)
        {
            boxCol.enabled = false;
            canJumpDown = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (Player.Instance.stateMachine.currentState == Player.Instance.jumpState)
        {
            Debug.Log("Bo may chuyen ho state day");
            Player.Instance.stateMachine.ChangeState(Player.Instance.idleState);
        }     
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Jump down!");
            canJumpDown = true;
            boxCol.enabled = false;
        }
    }
}
