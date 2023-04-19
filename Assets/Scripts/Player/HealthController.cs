using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthController : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField] private float currentHealth;
    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI sheetsTMP;
    [SerializeField] private int sheetsSaves;
    Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        sheetsTMP.text = sheetsSaves.ToString();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        HPBarUpdate();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentHealth < 120)
            {
                AddHealth(20);
                sheetsTMP.text = sheetsSaves.ToString();
            }
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Damage");
        HPBarUpdate();
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void AddHealth(float _value)
    {
        if (sheetsSaves > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
            sheetsSaves--;
            sheetsTMP.text = sheetsSaves.ToString();
        }
    }

    public void UpdateHoja()
    {
        sheetsSaves++;
        sheetsTMP.text = sheetsSaves.ToString();
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
