using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats: MonoBehaviour
{
    public int currentHealth;
    [SerializeField] private int maxHealth;
    public int damage;
    public int impactDamage;
    private void Start()
    {
        currentHealth = maxHealth;
    }
    public void GetDamageStat(float _damage)
    {
        currentHealth -= Mathf.RoundToInt(_damage);
        if(currentHealth <= 0 )
        {
            currentHealth = 0;
            Died();
        }
    }
    public void Died()
    {
        Debug.Log(gameObject.name + " died!");
        
    }
}
