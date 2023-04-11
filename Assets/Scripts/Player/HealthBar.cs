using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void ChangeMaxHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
    }

    public void ChangeCurrentHealth(float amountHealth)
    {
        slider.value = amountHealth;
    }

    public void InitialHealthBar(float amountHealth)
    {
        ChangeMaxHealth(amountHealth);
        ChangeCurrentHealth(amountHealth);
    }
}
