using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashShadow : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private float losingAlphaColorCooldown;
    private SpriteRenderer playerSprite;
    private void OnEnable()
    {
        sr = transform.Find("Model").GetComponent<SpriteRenderer>();
        playerSprite = Player.Instance.transform.Find("Model").GetComponent<SpriteRenderer>();
        sr.color = new Color(210f/255f, 140f/255f, 240f/255f, 1f);
        sr.sprite = playerSprite.sprite;
        StartCoroutine("LosingAlphaColor");
        if (Player.Instance.facingDir == -1)
            transform.Rotate(0f, 180f, 0f);
    }
    private void Update()
    {
        if(sr.color.a <= 0)
        {
            //despawn
            Spawner.instance.Despawn(transform);
        } 
    }
    private IEnumerator LosingAlphaColor()
    {
        while(sr.color.a > 0)
        {
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.1f);
            yield return new WaitForSeconds(losingAlphaColorCooldown);
        }
    }
}
