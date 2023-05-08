using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;

    private Animator animator;
    private GameObject player;
    private enemyPatrol enemyPatrol;
    public float damage = 10f;
    private float CurrentSpeed;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        enemyPatrol = GetComponent<enemyPatrol>();
        CurrentSpeed = enemyPatrol.speed;
    }

    public void TakeDamage(float damage)
    {
        life -= damage;
        animator.SetTrigger("Hurt");
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
            enemyPatrol.speed = 0f;
            animator.SetBool("AttackTrigger", true);
            animator.SetBool("isRunning", false);

        }
        else if (distanceToPlayer > 1.5f)
        {
            enemyPatrol.speed = CurrentSpeed;
            animator.SetBool("AttackTrigger", false);
            animator.SetBool("isRunning", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.KBCounter = player.KBTotalTime;
            if (collision.transform.position.x <= transform.position.x)
            {
                player.KnockFromRight = true;
            }
            if (collision.transform.position.x >= transform.position.x)
            {
                player.KnockFromRight = false;
            }
            collision.gameObject.GetComponent<HealthController>().TakeDamage(damage);
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        Debug.Log("Muere");
        Destroy(gameObject);
    }
}

