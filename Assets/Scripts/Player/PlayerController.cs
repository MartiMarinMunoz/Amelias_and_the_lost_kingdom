using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Settings")]
    public float velocity;     
    public float jumpForce;
    public float maximumsJumpsOnAir;
    public LayerMask Ground;

    private bool playerDirection = true;
    private Rigidbody2D rb;
    public BoxCollider2D bc;

    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Animator animator;
    private float jumpsRemaining;
     
    private void Start()
    {      
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maximumsJumpsOnAir;
    }
    void Update()
    {
        PlayerMovement();
        Jump(); 
    }

    private bool OnGround()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(bc.bounds.center, new Vector2(bc.bounds.size.x, bc.bounds.size.y), 0f, Vector2.down, 0.2f, Ground);
        return raycastHit.collider != null;
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

    private void PlayerMovement()
    {
        float move = Input.GetAxis("Horizontal"); 

        if(move != 0f)
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

    private void Flip(float move)
    {
        if((playerDirection == true && move < 0) || playerDirection == false && move > 0)
        {
            playerDirection = !playerDirection;
            sr.flipX = !playerDirection;
        }
    }
}