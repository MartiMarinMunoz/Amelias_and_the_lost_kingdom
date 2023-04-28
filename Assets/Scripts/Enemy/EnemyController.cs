using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;

    private Animator animator;
    public GameObject player;
    public float damage = 10f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Death();
        }
    }
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);



        if (distanceToPlayer < 1.5f)
        {
            animator.SetBool("AttackTrigger", true);

        }
        else if (distanceToPlayer > 1.5f)
        {
            animator.SetBool("AttackTrigger", false);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            //currentHealth playerHealth = player.GetComponent<currentHealth>();
            // playerHealth.TakeDamage(damage);
        }
    }
    private void Death()
    {
        animator.SetTrigger("Death");
    }
}

