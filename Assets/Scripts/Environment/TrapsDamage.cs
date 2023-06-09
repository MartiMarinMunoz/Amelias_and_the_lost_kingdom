using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapsDamage : MonoBehaviour
{
    [SerializeField] private float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            StartCoroutine(player.MoveCharacter(player.initialPosition, player.initialRotation));
            player.MoveCharacter(player.initialPosition, player.initialRotation);
            collision.GetComponent<HealthController>().TakeDamage(damage, collision.tag);
        }
    }
}
