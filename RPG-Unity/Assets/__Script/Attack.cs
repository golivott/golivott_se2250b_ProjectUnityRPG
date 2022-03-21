using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDist = 1f;
    public float attackRange = 1f;

    public Vector2 attackPoint;
    public LayerMask enemyLayers;

    void Update()
    {
        // Updating attack point
        if (transform.GetComponent<Movement>().moveDir != Vector2.zero)
        {
            attackPoint = transform.GetComponent<Movement>().moveDir * attackDist + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Attack1();
        }
    }

    void Attack1()
    {
        // Show attack sprite

        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemyLayers);
        
        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            print("hit: " + enemy);
        }
    }
}
