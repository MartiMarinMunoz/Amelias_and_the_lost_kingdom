using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;
    private bool isPaused;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Time.timeScale = 0;
            isPaused = true;
            OpenPanel(settingsPanel);
        }   
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            Time.timeScale = 1;
            isPaused = false;
            ClosePanel(settingsPanel);
        }
    }
    public void OpenPanel(GameObject panel)
    {
        Time.timeScale = 0;
        panel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
    }

    public void ClosePanel(GameObject panel)
    {
        Time.timeScale = 1;
        panel.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

}
