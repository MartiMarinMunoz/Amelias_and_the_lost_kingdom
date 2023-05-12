using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalLevel : MonoBehaviour
{
    [SerializeField] private GameObject PanelFinal;

    private void Start()
    {
        Time.timeScale = 1;
        PanelFinal.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Time.timeScale = 0;
        PanelFinal.SetActive(true);
    }
}
