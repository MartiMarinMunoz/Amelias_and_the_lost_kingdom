using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HealthCollectable : MonoBehaviour
{
    public AudioSource clip;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<HealthController>().UpdateHoja();
            Destroy(gameObject, 0.1f);

            clip.Play();
        }
    }
}
