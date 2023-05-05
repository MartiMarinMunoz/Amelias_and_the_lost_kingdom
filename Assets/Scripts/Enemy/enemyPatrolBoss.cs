using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrolBoss : MonoBehaviour
{
    public GameObject pointC;
    public GameObject pointD;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    public Transform playerTransform;
    public bool isChasing;
    public float chaseDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = pointD.transform;
        anim.SetBool("isRunning", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isChasing)
        {
            if (transform.position.x > playerTransform.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
                transform.position += Vector3.left * speed * Time.deltaTime;
            }
            if (transform.position.x < playerTransform.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                transform.position += Vector3.right * speed * Time.deltaTime;
            }
        }
        else
        {
            if (Vector2.Distance(transform.position, playerTransform.position) < chaseDistance)
            {
                isChasing = true;
            }

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointD.transform)
            {
                rb.velocity = new Vector2(speed, 0);
            }
            else
            {
                rb.velocity = new Vector2(-speed, 0);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointD.transform)
            {
                flip();
                currentPoint = pointC.transform;
            }
            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointC.transform)
            {
                flip();
                currentPoint = pointC.transform;
            }
        }



    }

    private void flip()
    {
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(pointC.transform.position, 0.5f);
        Gizmos.DrawWireSphere(pointD.transform.position, 0.5f);
        Gizmos.DrawLine(pointC.transform.position, pointD.transform.position);
    }
}
