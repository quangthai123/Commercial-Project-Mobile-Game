using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class PlayerEffectDespawnByAnim : MonoBehaviour
{
    public bool canFlip;
    public bool wrongDirWhenSpawn = false;
    public void Flip() 
    {
        transform.Rotate(0f, 180f, 0f);
    }
    private void DespawnAfterFinishAnim()
    {
        PlayerEffectSpawner.instance.Despawn(transform);
    }
}
