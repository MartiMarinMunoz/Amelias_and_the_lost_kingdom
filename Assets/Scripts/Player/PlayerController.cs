using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public LayerMask Ground;
    public float velocity;
    private bool playerDirection = true;
    private Rigidbody2D rb;
    public BoxCollider2D feets;
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;

    [Header("Jumping Settings")]
    public float jumpForce;
    public float maximumsJumpsOnAir;
    private float jumpsRemaining;

    [Header("Attcak Settings")]
    [SerializeField] private Transform damageController;
    [SerializeField] private float damageRadio;
    [SerializeField] private float damage;   
    [SerializeField] private float nextAttack;
    [SerializeField] private GameObject meleeRadio;

    private float cooldownAttack;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maximumsJumpsOnAir;
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

        if (move != 0f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        rb.velocity = new Vector2(move * velocity, rb.velocity.y);
        Flip(move);
    }
    private void Jump()
    {
        if (OnGround())
        {
            jumpsRemaining = maximumsJumpsOnAir;
            animator.SetBool("isJumping", false);
        }

        else
        {
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && jumpsRemaining > 0)
        {
            jumpsRemaining--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
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
        if((playerDirection == true && move < 0) || playerDirection == false && move > 0)
        {
            playerDirection = !playerDirection;
            sr.flipX = !playerDirection;
            meleeRadio.transform.position = new Vector2(meleeRadio.transform.position.x * -1, 0);
        }
    }
    private void Attack()
    {
        if(cooldownAttack > 0)
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
        animator.SetTrigger("Damage");
        Collider2D[] objects = Physics2D.OverlapCircleAll(damageController.position, damageRadio);

        foreach (Collider2D colisioned in objects)
        {
            if (colisioned.CompareTag("Enemy"))
            {
                colisioned.transform.GetComponent<EnemyController>().TakeDamage(damage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(damageController.position, damageRadio);
    }

}