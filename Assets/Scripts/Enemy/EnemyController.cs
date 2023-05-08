using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float life;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float enemySpeed;

    private Animator animator;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Movment Settings")]
    public float movmentSpeed = 5f;
    public List<Transform> patrolList;
    int currentPatrolPoint = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
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
        if (Vector2.Distance(transform.position, patrolList[currentPatrolPoint].position) < 0.1f)
        {
            currentPatrolPoint++;

            if (currentPatrolPoint >= patrolList.Count)
            {
                currentPatrolPoint = 0;
            }

            if (patrolList[currentPatrolPoint].position.x > transform.position.x)
            {
                Flip(0);
            }
            else
            {
                Flip(180);
            }
        }

        //if (distanceToPlayer < 1.5f)
        //{
        //    animator.SetBool("AttackTrigger", true);

        //}
        //else if (distanceToPlayer > 1.5f)
        //{
        //    animator.SetBool("AttackTrigger", false);
        //}
    }

    private void FixedUpdate()
    {
        rb.transform.position = Vector2.MoveTowards(transform.position, patrolList[currentPatrolPoint].position, movmentSpeed * Time.fixedDeltaTime);
        animator.SetBool("isRunning", true);
    }

    void Flip(int value)
    {
        transform.eulerAngles = new Vector3(0, value, 0);
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        Debug.Log("Muere");
        Destroy(gameObject);
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

    
}

