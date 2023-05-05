using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    private float timer;
    private Animator animator;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        

        float distance = Vector2.Distance(transform.position, player.transform.position);
        


        if(distance < 6)
        {
            timer += Time.deltaTime;

            if (timer > 1.5)
            {
                timer = 0;
                shoot();
            }
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer < 7f)
        {
            animator.SetBool("isTrigger", true);

        }
        else if (distanceToPlayer > 7f)
        {
            animator.SetBool("isTrigger", false);
        }


    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }
}
