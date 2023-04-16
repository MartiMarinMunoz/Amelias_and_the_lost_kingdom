using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider healthBar;

    void Start()
    {
        healthBar = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(float maxHealth)
    {
        healthBar.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float amountHealth)
    {
        healthBar.value = amountHealth;
    }

    public void StartHealthBar(float amountHealth)
    {
        ChangeMaxHealth(amountHealth);
        ChangeCurrentHealth(amountHealth);
    }
}
