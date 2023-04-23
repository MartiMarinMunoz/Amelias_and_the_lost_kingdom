using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollsController : MonoBehaviour
{
    public GameObject panel;
    private bool inColision = false;
    private bool activePanel = false;
    private PlayerController player;
    private SettingsController settings;

    [SerializeField] private GameObject text;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        settings = GameObject.FindGameObjectWithTag("UI").GetComponent<SettingsController>();
        text.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inColision = true;
            text.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inColision = false;
            text.SetActive(false);
        }
    }

    void Update()
    {
        if (inColision && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = activePanel ? 1 : 0;
            activePanel = !activePanel;
            player.enabled = !activePanel;
            settings.enabled = !activePanel;
            panel.SetActive(activePanel);
        }
    }
}
