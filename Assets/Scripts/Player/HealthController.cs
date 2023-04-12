using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        health = maxHealth;
        healthBar.StartHealthBar(health);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.ChangeCurrentHealth(health);
        if(health < 0)
        {
            Destroy(gameObject);
        }
    }
    
}
