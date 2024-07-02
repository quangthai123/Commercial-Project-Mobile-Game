using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Stat damage; // player: original 10 update 40 // nang kiem tai lv: 1 3 4 5 6 7
    public Stat maxHealth; // player: original 100 update 250 // item nang hp tai lv: 1 3 5 6 8// item nang so binh mau tai 1-8
    public Stat resistantRate;
    public float currentHealth;
    private void Start()
    {
        currentHealth = maxHealth.GetValue();

    }
    private void Update()
    {
        //currentHealth = maxHealth.GetValue();
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    AddMaxHealth(30);
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //    DeductMaxHealth(30);
    }
    public void GetDamageStat(int _damage)
    {
        float finalDamage = _damage;
        finalDamage -= finalDamage*resistantRate.GetValue()/100f;
        currentHealth -= Mathf.RoundToInt(finalDamage);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }
    public void AddMaxHealth(float _hp)
    {
        maxHealth.AddModifier(_hp);
    }
    public void DeductMaxHealthByItem(float _hp)
    {
        maxHealth.RemoveModifier(_hp);
    }
}
