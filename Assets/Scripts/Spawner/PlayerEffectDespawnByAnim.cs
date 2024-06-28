using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectDespawnByAnim : MonoBehaviour
{
    [SerializeField] private bool canFlip = false;
    [SerializeField] private int facingDir;
    [SerializeField] private int facingDirOriginal;
    private void Flip()
    {
        transform.Rotate(0f, 180f, 0f);
        facingDir *= -1;
    }
    void OnEnable()
    {
        if (Player.Instance.facingDir != facingDir && canFlip)
        {
            Flip();
        }
    }
    private void OnDisable()
    {
        if(facingDir != facingDirOriginal)
        {
            Flip();
        }
    }
    void Update()
    {
        
    }
    private void DespawnAfterFinishAnim()
    {
        PlayerEffectSpawner.instance.Despawn(transform);
    }
}
