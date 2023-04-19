using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private LayerMask Ground;
    [SerializeField] private float velocity;
    [SerializeField] private bool playerDirection = true;
    private Rigidbody2D rb;
    public BoxCollider2D feets;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;

    [Header("Jumping Settings")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float maximumsJumps;
    private float jumpsRemaining;

    [Header("Attcak Settings")]
    [SerializeField] private Transform damageController;
    [SerializeField] private float damageRadio;
    [SerializeField] private float damage;
    [SerializeField] private float nextAttack;
    //[SerializeField] private GameObject meleeRadio;

    public Vector2 initialPosition { get; set; }
    public Quaternion initialRotation { get; set; }

    private float cooldownAttack;

    HealthController health;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<HealthController>();
        jumpsRemaining = maximumsJumps;
        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }
    void Update()
    {
        PlayerMovement();
        Jump();
        Attack();
    }
    private void PlayerMovement()
    {
        float move = Input.GetAxis("Horizontal");

        Flip(move);
        animator.SetBool("isRunning", Mathf.Abs(move) > 0.1f);

        rb.velocity = new Vector2(move * velocity, rb.velocity.y);
    }
    private void Jump()
    {
        if (OnGround())
        {
            jumpsRemaining = maximumsJumps;
            animator.SetBool("isJumping", false);
        }
        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    private bool OnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(feets.bounds.center, new Vector2(feets.bounds.size.x, feets.bounds.size.y), 0f, Vector2.down, 0.2f, Ground);
        return raycastHit.collider != null;
    }

    private void Flip(float move)
    {
        if ((playerDirection == true && move < 0) || playerDirection == false && move > 0)
        {
            playerDirection = !playerDirection;
            sr.flipX = !playerDirection;
            damageController.transform.localPosition = new Vector2(damageController.transform.localPosition.x * -1, damageController.transform.localPosition.y);
        }
    }

    private void Attack()
    {
        if (cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Fire1") && cooldownAttack <= 0)
        {
            OnAttack();
            cooldownAttack = nextAttack;
        }
    }
    private void OnAttack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] objects = Physics2D.OverlapCircleAll(damageController.position, damageRadio);

        foreach (Collider2D colisioned in objects)
        {
            if (colisioned.CompareTag("Enemy"))
            {
                colisioned.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }
    public void MoveCharacter(Vector2 position, Quaternion rotation)
    {
        gameObject.transform.Translate(Vector2.zero);

        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        transform.position = position;
        transform.rotation = rotation;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageController.position, damageRadio);
    }

}