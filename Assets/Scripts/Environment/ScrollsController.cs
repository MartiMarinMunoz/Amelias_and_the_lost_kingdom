using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollsController : MonoBehaviour
{
    public GameObject panel; 
    private bool inColision = false; 
    private bool activePanel = false; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            inColision = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {  
            inColision = false;
        }
    }

    void Update()
    {
        if (inColision && Input.GetKeyDown(KeyCode.E))
        {
            Time.timeScale = activePanel?1:0;
            activePanel = !activePanel;
            panel.SetActive(activePanel);        
        }
    }
}
