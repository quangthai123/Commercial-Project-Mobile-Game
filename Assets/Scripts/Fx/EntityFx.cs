using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    private Material originalMat;
    private SpriteRenderer sr;
    [SerializeField] private Material flashFxMat;
    [SerializeField] private float flashFxDuration;
    private Color originalColor;
    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMat = sr.material;
        originalColor = sr.color;
    }
    public IEnumerator FlashFX()
    {
        sr.material = flashFxMat;
        yield return new WaitForSeconds(flashFxDuration);
        sr.material = originalMat;
    }
    public IEnumerator BeCounterAttackedFlashFx()
    {
        sr.color = new Color(255f / 255f, 0f, 0f);
        yield return new WaitForSeconds(flashFxDuration);
        sr.color = originalColor;
    } 
}
