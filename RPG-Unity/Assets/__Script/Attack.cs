using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDist = 1f;
    public float attackRange = 1f;
    
    public LayerMask enemyLayers;
    public GameObject swipeAttack;
    
    [Header("Set Dynamically")]
    public Vector2 attackPoint;
    public Vector2 lastMoveDir;    

    void Update()
    {
        // Updating attack point
        if (GetComponent<Movement>().moveDir != Vector2.zero)
        {
            lastMoveDir = GetComponent<Movement>().moveDir;
            attackPoint = GetComponent<Movement>().moveDir * attackDist + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Attack1();
        }
    }

    void Attack1()
    {
        // Show attack sprite
        GameObject attackSprite = swipeAttack;
        attackSprite.transform.position = attackPoint;
        attackSprite.transform.rotation = Quaternion.EulerAngles(0,0,(float)Math.Tan(Convert.ToDouble(lastMoveDir.y/lastMoveDir.x)));
        Instantiate(attackSprite);
        Destroy(attackSprite,0.5f);

        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attackPoint, attackRange, enemyLayers);
        
        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            print("hit: " + enemy);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackPoint,attackRange);
    }
}
