using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] private int life;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    private float timer;
    private Animator animator;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);

        if(distance < 6)
        {
            timer += Time.deltaTime;

            
            if(timer > 1f)
                animator.SetTrigger("Attack");
            if (timer > 1.5f)
            {
                timer = 0;
                shoot();
            }
        }

        //float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        //if (distanceToPlayer < 7f)
        //{
        //    animator.SetTrigger("Attack");

        //}
    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
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

    public void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 0.35f);
    }
}
