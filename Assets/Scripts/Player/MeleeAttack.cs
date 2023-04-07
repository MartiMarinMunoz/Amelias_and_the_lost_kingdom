using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private Transform meleeController;
    [SerializeField] private float meleeRadio;
    [SerializeField] private float meleeDamage;

    private void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Melee();
        }
    }
    
    private void Melee()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(meleeController.position, meleeRadio);

        foreach (Collider2D colision in objects)
        {
            if (colision.CompareTag("Enemy"))
            {
                //colision.transform.GetComponent<Enemy>().TakeDamage(meleeDamage);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(meleeController.position, meleeRadio);
    }
}
