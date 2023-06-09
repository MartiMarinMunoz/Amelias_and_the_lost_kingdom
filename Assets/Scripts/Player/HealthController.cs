using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HealthController : MonoBehaviour
{

    [SerializeField] private float maxHealth;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI sheetsTMP;
    [SerializeField] private int sheetsSaves;
    [SerializeField] private Animator anim;
    
    private SettingsController settingsController;
    private float currentHealth;


    void Start()
    {
        settingsController = GameObject.FindGameObjectWithTag("UI").GetComponent<SettingsController>();
        currentHealth = maxHealth;
        anim = GetComponentInChildren<Animator>();
        sheetsTMP.text = sheetsSaves.ToString();
    }

    private void Update()
    {
        HPBarUpdate();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (currentHealth < 120)
            {
                AddHealthSheets(20);
                sheetsTMP.text = sheetsSaves.ToString();
            }
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            settingsController.isDeath = true;
            //Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage, string tag)
    {
        if (gameObject.tag != tag)
            return;

        currentHealth -= damage;
        anim.SetTrigger("Damage");
        HPBarUpdate();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            settingsController.isDeath = true;
            //anim.SetTrigger("Death");
            //Destroy(gameObject);
        }
    }


    public void AddHealthSheets(float _value)
    {
        if (sheetsSaves > 0)
        {
            currentHealth = Mathf.Clamp(currentHealth + _value, 0, maxHealth);
            sheetsSaves--;
            sheetsTMP.text = sheetsSaves.ToString();
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
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