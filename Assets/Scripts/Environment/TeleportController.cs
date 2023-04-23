using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour
{
    [SerializeField] private Transform destination; 
    [SerializeField] private GameObject text;
    private bool inColision = false;

    private void Start()
    {
        text.SetActive(false);
    }
    public Transform GetDestination()
    {
        return destination;
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
}
