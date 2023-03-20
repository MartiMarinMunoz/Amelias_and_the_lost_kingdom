using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public enum playerType { Endless, Platformer }

    [Header("PlayerType")]
    public playerType currentPlayerType;

    [Header("Movment settings")]
    public float movementSpeed = 5f;
    public float movementSmoohting = 0.1f;
    Vector3 velocity = Vector3.zero;
    float inputX = 0;

    [Header("Jumping settings")]
    public float jumpForce;
    public Transform checkGround;
    public float checkGroundRadio;
    public LayerMask groundLayer;

    public int jumpsOnAir = 1;
    int currentJumpsOnAir = 0;

    [Header("Melee Attack settings")]
    public Transform meleeAttackPoint;
    public float meleeAttackRadio;
    public LayerMask enemyLayer;
    public int meleeDamage;

    //UIController _UIController;
    bool gameOver;

    Rigidbody2D rb;
    Vector2 initialPosition;

    Animator anim;

    //public int currentWorld = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        //currentGravity = rb.gravityScale;

        initialPosition = transform.position;

        anim = GetComponent<Animator>();

        //_UIController = GameObject.FindGameObjectWithTag("UI").GetComponent<UIController>();
    }

    void Update()
    {
        if (gameOver)
        {
            return;
        }

        Flip();
        GeneralInput();
        Jump();
        SetAnimation();
    }

    private void FixedUpdate()
    {
        Move(inputX);
    }

    #region GeneralInput
    void GeneralInput()
    {
        if (currentPlayerType == playerType.Endless)
            inputX = 1;
        else
            inputX = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Fire1"))
        {
            anim.Play("PlayerAttack");
            Attack(true);
        }
    }
    #endregion

    #region Animation
    private void SetAnimation()
    {
        StartCoroutine(playAnimations());

        //anim.SetBool("OnGround", OnGround());
        //anim.SetBool("Run", inputX != 0);      
    }

    IEnumerator playAnimations()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("PlayerAttack"))
        {  
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        }
        else if (!OnGround())
        {
            anim.Play("PlayerJump");
        }
        else if (inputX != 0)
        {
            anim.Play("PlayerRun");
        }
        else
        {
            anim.Play("PlayerIdle");
        }

        yield return null;
    }
    #endregion

    #region Attack
    void Attack(bool melee)
    {
        if (melee)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttackPoint.position, meleeAttackRadio, enemyLayer);

            foreach (Collider2D enemy in hitEnemies)
            {
                if(enemy.GetComponent<HealthManager>() != null)
                {
                    enemy.GetComponent<HealthManager>().TakeDamage(meleeDamage, "Enemy");
                }    
            }
        }
    }

    #endregion

    #region Jump
    void Jump()
    {
        if (OnGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
            currentJumpsOnAir = 0;
        }
        else
        {
            if (Input.GetButtonDown("Jump") && currentJumpsOnAir < jumpsOnAir)
            {
                currentJumpsOnAir++;

                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }
    }
    #endregion

    #region Movement
    private void Move(float value)
    {
        Vector3 targetVelocity = new Vector2(value * movementSpeed, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, movementSmoohting);
    }
    #endregion

    #region OnGround
    private bool OnGround()
    {
        return Physics2D.OverlapCircle(checkGround.position, checkGroundRadio, groundLayer);
    }

    #endregion

    #region DeadZone
    public void ResetGame()
    {

        //SceneManager.LoadScene(0);

        gameOver = true;
        //_UIController.SetGameOver();

        rb.velocity = Vector2.zero;
        transform.localScale = Vector3.one;
        transform.position = initialPosition;
    }
    #endregion

    #region Flip
    public void Flip()
    {
        if (inputX > 0)
        {
            transform.localScale = Vector3.one;
            Camera.main.GetComponent<CameraController>().Rightoffset = true;
        }

        if (inputX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            Camera.main.GetComponent<CameraController>().Rightoffset = false;
        }
    }
    #endregion

    #region DrawingGizmos
    private void OnDrawGizmos()
    {
        if (OnGround())
        {
            Gizmos.color = Color.green;
        }
        else
        {
            Gizmos.color = Color.red;
        }

        Gizmos.DrawWireSphere(checkGround.position, checkGroundRadio);

        Gizmos.DrawWireSphere(meleeAttackPoint.position, meleeAttackRadio);
    }

    #endregion


    //public UIController ui;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{

    //    if (collision.gameObject.tag == "FinalLevel")
    //    {
    //        //SceneManager.LoadScene(0);
    //        ui.SetLevelComplete();
    //    }

    //}
}
