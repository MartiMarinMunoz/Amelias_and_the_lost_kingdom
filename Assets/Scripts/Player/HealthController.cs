using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.StartHealthBar(currentHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.ChangeCurrentHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth( float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
    }
    
}
