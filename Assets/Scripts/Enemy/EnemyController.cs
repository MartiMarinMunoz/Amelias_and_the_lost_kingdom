using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float life;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        life -= damage;

        if(life <= 0 )
        {
            Death();
        }
    }

    private void Death()
    {
        animator.SetTrigger("Death");
    }
}
