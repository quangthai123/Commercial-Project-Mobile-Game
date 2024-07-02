using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager Instance;
    private Transform enemies;
    public Dictionary<Transform, bool> enemiesList;
    void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;
        enemies = transform.Find("Enemies");
        foreach(Transform enemy in enemies)
        {
            enemiesList.Add(enemy, true);
        }
    }
    
}
