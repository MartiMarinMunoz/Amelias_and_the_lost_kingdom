using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject DeathPanel;
    [SerializeField] private PlayerController player;
    [SerializeField] private HealthController playerHealt;
    private bool isPaused;
    public bool isDeath { get; set; }

    private void Start()
    {
        isPaused = false;
        isDeath = false;
        Time.timeScale = 1;
        DeathPanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isDeath == false)
        {
            SetPause();
        }
        if(isDeath)
        {
            DeathPlayer();
        }

    }
    public void SetPause()
    {
        isPaused = !isPaused;
        settingsPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        player.enabled = !isPaused;
    }

    private void DeathPlayer()
    {
        DeathPanel.SetActive(true);
        player.enabled = false;
        Time.timeScale = 0;
    }

    public void Rebot()
    {
        Time.timeScale = 1;
        isDeath = false;
        player.enabled = true;
        playerHealt.AddHealth(120);
        DeathPanel.SetActive(false);
        StartCoroutine(player.MoveCharacter(player.initialPosition, player.initialRotation));
    }
}
