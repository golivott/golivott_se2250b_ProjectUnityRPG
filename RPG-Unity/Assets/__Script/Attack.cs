using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDist = 1f;
    public float attack1Range = 1f;
    public float attack2Range = 1f;
    public float attackDelay = 0.5f;

    public LayerMask enemyLayers;
    public GameObject swipeAttack;

    [Header("Set Dynamically")]
    public Vector2 attack1Point;
    public Vector2 attack2Point;
    public Vector2 lastMoveDir;
    public bool canAttack = true;

    private void Start()
    {
        Physics2D.queriesHitTriggers = true;
    }

    void Update()
    {
        // Updating attack point
        if (GetComponent<Movement>().moveDir != Vector2.zero)
        {
            lastMoveDir = GetComponent<Movement>().moveDir;
        }
        attack1Point = lastMoveDir * attackDist + new Vector2(transform.position.x, transform.position.y);
        attack2Point = lastMoveDir * attackDist*attack2Range/2 + new Vector2(transform.position.x, transform.position.y);
        

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack1());
            }
        }
        
        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack2());
            }
        }
    }

    IEnumerator Attack1()
    {
        // Display animation
        GameObject attack1Sprite = Instantiate(swipeAttack);;
        attack1Sprite.transform.position = attack1Point;
        attack1Sprite.transform.rotation = Quaternion.EulerAngles(0,0,Mathf.Atan2(lastMoveDir.y,lastMoveDir.x));
        Destroy(attack1Sprite,0.1f);
        
        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attack1Point, attack1Range, enemyLayers);
        
        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            print("hit: " + enemy.name);
        }
        
        yield return new WaitForSecondsRealtime(attackDelay);

        canAttack = true;
    }

    IEnumerator Attack2()
    {
        // Display animation
        GameObject attack2Sprite = Instantiate(swipeAttack);;
        attack2Sprite.transform.position = attack1Point;
        attack2Sprite.transform.rotation = Quaternion.EulerAngles(0,0,Mathf.Atan2(lastMoveDir.y,lastMoveDir.x));
        attack2Sprite.AddComponent<Rigidbody>().velocity = lastMoveDir*2*attack2Range/0.4f;
        Destroy(attack2Sprite,0.2f);
        
        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapBoxAll(attack2Point, new Vector2(2*attack2Range,1), Mathf.Atan2(lastMoveDir.y,lastMoveDir.x) , enemyLayers);
        
        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            print("hit: " + enemy.name);
        }
        
        yield return new WaitForSecondsRealtime(attackDelay);

        canAttack = true;
    }
}
