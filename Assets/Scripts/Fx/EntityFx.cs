using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private Material originalMat;
    private SpriteRenderer sr;
    [SerializeField] private Material flashFxMat;
    [SerializeField] private float flashFxDuration;
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
    }
    public IEnumerator FlashFX()
    {
        sr.material = flashFxMat;
        yield return new WaitForSeconds(flashFxDuration);
        sr.material = originalMat;
    }
}
