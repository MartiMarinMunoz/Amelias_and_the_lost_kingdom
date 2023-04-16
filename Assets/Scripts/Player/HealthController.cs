using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI hojasTMP;
    [SerializeField] private int HojasSaves;

    void Start()
    {
        currentHealth = maxHealth;
        hojasTMP.text = HojasSaves.ToString();
    }

    private void Update()
    {
        HPBarUpdate();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentHealth < 120)
            {
                AddHealth(20);
                hojasTMP.text = HojasSaves.ToString();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        HPBarUpdate();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(float _value)
    {
        if (HojasSaves > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
            HojasSaves--;
            hojasTMP.text = HojasSaves.ToString();
        }
    }

    public void UpdateHoja()
    {
        HojasSaves++;
        hojasTMP.text = HojasSaves.ToString();
    }

    public void HPBarUpdate()
    {
        healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
        if (currentHealth > 120)
        {
            currentHealth = 120;
        }
    }
}
