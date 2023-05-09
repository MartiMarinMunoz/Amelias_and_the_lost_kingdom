using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private float life;
    [SerializeField] private float damage = 10f;

    private Animator animator;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Movment Settings")]
    [SerializeField] private float curretnSpeed = 2f;
    [SerializeField] private List<Transform> patrolList;

    int currentPatrolPoint = 0;
    float speedEnemy;

    [Header("Atakc Range")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform damageController;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 1f;

    private bool isInRange = false;
    private float attackTimer = 0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        transform.position = patrolList[currentPatrolPoint].position;
        animator.SetBool("isRunning", true);
        speedEnemy = curretnSpeed;
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
        EnemyMovement();
        PlayerFollow();
    }

    private void FixedUpdate()
    {
        rb.transform.position = Vector2.MoveTowards(transform.position, patrolList[currentPatrolPoint].position, curretnSpeed * Time.fixedDeltaTime);
    }

    private void EnemyMovement()
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
    }

    private void PlayerFollow()
    {
        Vector3 targetPosition = patrolList[currentPatrolPoint].position;
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < detectionRange)
            {
                isInRange = true;
                if (distanceToPlayer > attackRange)
                {
                    targetPosition = player.transform.position;
                }
                else
                {
                    if (attackTimer <= 0f)
                    {
                        OnAttack();
                        animator.SetBool("isRunning", false);
                        curretnSpeed = 0f;
                        attackTimer = attackDelay;
                    }
                    else
                    {
                        attackTimer -= Time.deltaTime;
                    }
                }
            }
            else
            {
                isInRange = false;
                animator.SetBool("isRunning", true);
                curretnSpeed = speedEnemy;
            }
        }

        if (isInRange)
        {
            //animator.SetBool("isRunning", false);
            //curretnSpeed = 0f;
        }

        Vector3 direction = targetPosition - transform.position;
        direction.Normalize();

        transform.position += direction * +curretnSpeed * Time.deltaTime;
    }

    private void OnAttack()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D[] objects = Physics2D.OverlapCircleAll(damageController.position, attackRange);

        foreach (Collider2D colisioned in objects)
        {
            if (colisioned.CompareTag("Player"))
            {
                colisioned.transform.GetComponent<HealthController>().TakeDamage(damage);
            }
        }
    }

    void Flip(int value)
    {
        transform.eulerAngles = new Vector3(0, value, 0);
    }

    private void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageController.position, attackRange);
    }
}

