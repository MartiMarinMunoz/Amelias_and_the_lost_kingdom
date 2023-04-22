using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject optionsPanel;

    private void Start()
    {
        optionsPanel.SetActive(false);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Level()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenPanel(GameObject panel)
    {       
        panel.SetActive(true);     
    }

    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }
}
