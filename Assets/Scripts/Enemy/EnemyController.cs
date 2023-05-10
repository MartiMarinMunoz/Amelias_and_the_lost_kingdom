using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private int life;
    [SerializeField] private float damage = 10f;

    private Animator animator;
    private GameObject player;
    private Rigidbody2D rb;

    [Header("Movment Settings")]
    [SerializeField] private float curretnSpeed = 2f;
    [SerializeField] private List<Transform> patrolList;

    int currentPatrolPoint = 0;
    float speedEnemy;

    [Header("Ground Detection settings")]
    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask groundLayer;
    float distance = 0.25f;
    RaycastHit2D groundInfo;

    [Header("Atakc Range")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private Transform damageController;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackDelay = 0.25f;

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

    
    void Update()
    {
        EnemyMovement();
        PlayerFollow();
    }

    private void FixedUpdate()
    {
        Vector2 target;
        if (OnGrount())
        {
            if (isInRange)
                target = new Vector2(player.transform.position.x, transform.position.y);
            else
                target = patrolList[currentPatrolPoint].position;
        }
        else
        {
            target = patrolList[currentPatrolPoint].position;
            isInRange = false;
        }

        rb.transform.position = Vector2.MoveTowards(transform.position, target, curretnSpeed * Time.fixedDeltaTime);

        if (target.x > transform.position.x)
            Flip(0);
        else if (target.x < transform.position.x)
            Flip(180);
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

    private bool OnGrount()
    {
        groundInfo = Physics2D.Raycast(groundPoint.position, Vector2.down, distance, groundLayer);
        Debug.DrawRay(groundPoint.position, Vector2.down, Color.red, distance);

        if (!groundInfo.collider)
        {
            return false;
        }

        return true;
    }

    private void PlayerFollow()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            if (distanceToPlayer < detectionRange)
            {
                isInRange = true;
                if (distanceToPlayer < attackRange)
                {
                    animator.SetBool("isRunning", false);
                    curretnSpeed = 0f;
                    if (attackTimer <= 0f)
                    {
                        OnAttack();
                        attackTimer = attackDelay;
                    }
                    else
                    {
                        attackTimer -= Time.deltaTime;
                    }
                }
                else
                {
                    animator.SetBool("isRunning", true);
                    curretnSpeed = speedEnemy;
                }
            }
            else
            {
                isInRange = false;
                animator.SetBool("isRunning", true);
                curretnSpeed = speedEnemy;
            }
        }
    }

    private void OnAttack()
    {
        animator.SetTrigger("AttackTrigger");
        Collider2D[] objects = Physics2D.OverlapCircleAll(damageController.position, attackRange);
        foreach (Collider2D colisioned in objects)
        {
            if (colisioned.CompareTag("Player"))
            {
                colisioned.transform.GetComponent<HealthController>().TakeDamage(damage, colisioned.tag);
            }
        }
    }

    void Flip(int value)
    {
        transform.eulerAngles = new Vector3(0, value, 0);
    }

    public void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject);
    }
    public void TakeDamage(int damage)
    {
        life -= damage;
        animator.SetTrigger("Hurt");
        if (life <= 0)
        {
            life = 0;
            Death();
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageController.position, attackRange);
    }
}

