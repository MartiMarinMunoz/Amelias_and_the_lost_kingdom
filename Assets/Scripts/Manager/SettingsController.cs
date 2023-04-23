using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{

    [Header("Panel")]
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private PlayerController player;
    private bool isPaused;
    public bool isDead { get; set; }

    private void Start()
    {
        isPaused = false;
        isDead = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && isDead == false)
        {
            SetPause();
        }
        //if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        //{
        //    Time.timeScale = 0;
        //    isPaused = true;
        //    OpenPanel(settingsPanel);
        //}   
        //else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        //{
        //    Time.timeScale = 1;
        //    isPaused = false;
        //    ClosePanel(settingsPanel);
        //}
    }
    public void SetPause()
    {
        isPaused = !isPaused;
        settingsPanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
        player.enabled = !isPaused;
    }
    //public void OpenPanel(GameObject panel)
    //{
    //    Time.timeScale = 0;
    //    panel.SetActive(true);
    //    Cursor.lockState = CursorLockMode.None;
    //}

    //public void ClosePanel(GameObject panel)
    //{
    //    Time.timeScale = 1;
    //    panel.SetActive(false);
    //    Cursor.lockState = CursorLockMode.Locked;
    //}

}
