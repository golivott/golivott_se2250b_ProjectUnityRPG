using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTwo : Player
{
    // Properties for attack 1
    [Header("Attack 1")]
    public float attack1Damage = 25f;
    public float attack1Delay = 1f;
    public float attack1Speed = 750f;
    
    // Properties for attack 2
    [Header("Attack 2")]
    public float attack2Damage = 25f;
    public float attack2Delay = 2f;
    public float attack2Speed = 750f;
    
    public LayerMask enemyLayers;
    public GameObject arrowSprite;

    // Variables set by the script
    [Header("Set Dynamically")] 
    public Vector2 attackPoint;
    public bool canAttack = true;
    
    // Update is called once per frame
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        // Updating attack point
        if (moveDir != Vector2.zero)
        {
            lastMoveDir = moveDir;
        }
        attackPoint = new Vector2(transform.position.x, transform.position.y);
        
        // Listening for attack 1
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (canAttack)
            {
                canAttack = false;
                StartCoroutine(Attack1());
            }
        }

        // Listening for attack 2
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
        GameObject arrowSprite = Instantiate(this.arrowSprite);
        arrowSprite.transform.position = attackPoint;
        arrowSprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
            
        // Setting proporties of attack
        arrowSprite.GetComponent<Rigidbody2D>().velocity = attack1Speed * lastMoveDir.normalized * Time.fixedDeltaTime;
        arrowSprite.GetComponent<ProjectileAttack>().SetDamage(attack1Damage * GetDamageMultiplier());
        
        // Destroying attack
        Destroy(arrowSprite, 1f);
        
        yield return new WaitForSecondsRealtime(attack1Delay);
        canAttack = true;
    }

    IEnumerator Attack2()
    {
        // Spawn Bottom Arrow
        GameObject arrowSprite1 = Instantiate(arrowSprite);
        arrowSprite1.transform.position = attackPoint;
        arrowSprite1.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) - Mathf.PI/12);
        
        // Bottom Arrow Properties
        Vector2 arrow1Vector = new Vector2(
            lastMoveDir.x * Mathf.Cos(23 * Mathf.PI / 12) - lastMoveDir.y * Mathf.Sin(23 * Mathf.PI / 12),
            lastMoveDir.x * Mathf.Sin(23 * Mathf.PI / 12) + lastMoveDir.y * Mathf.Cos(23 * Mathf.PI / 12));
        arrowSprite1.GetComponent<Rigidbody2D>().velocity = attack2Speed * arrow1Vector.normalized * Time.fixedDeltaTime;
        arrowSprite1.GetComponent<ProjectileAttack>().SetDamage(attack2Damage * GetDamageMultiplier());

        yield return new WaitForSecondsRealtime(0.1f);
        
        // Spawn Middle Arrow
        GameObject arrowSprite2 = Instantiate(arrowSprite);
        arrowSprite2.transform.position = attackPoint;
        arrowSprite2.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) );
        
        // Middle Arrow Properties
        arrowSprite2.GetComponent<Rigidbody2D>().velocity = attack2Speed * lastMoveDir.normalized * Time.fixedDeltaTime;
        arrowSprite2.GetComponent<ProjectileAttack>().SetDamage(attack2Damage * GetDamageMultiplier());
        
        yield return new WaitForSecondsRealtime(0.1f);
        
        // Spawn Top Arrow
        GameObject arrowSprite3 = Instantiate(arrowSprite);
        arrowSprite3.transform.position = attackPoint;
        arrowSprite3.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x) + Mathf.PI/12);
        
        // Top Arrow Properties
        Vector2 arrow3Vector = new Vector2(
            lastMoveDir.x * Mathf.Cos(Mathf.PI / 12) - lastMoveDir.y * Mathf.Sin(Mathf.PI / 12),
            lastMoveDir.x * Mathf.Sin(Mathf.PI / 12) + lastMoveDir.y * Mathf.Cos(Mathf.PI / 12));
        arrowSprite3.GetComponent<Rigidbody2D>().velocity = attack2Speed * arrow3Vector.normalized * Time.fixedDeltaTime;
        arrowSprite3.GetComponent<ProjectileAttack>().SetDamage(attack2Damage * GetDamageMultiplier());
        
        // Destroying attack
        Destroy(arrowSprite1, 1f);
        Destroy(arrowSprite2, 1f);
        Destroy(arrowSprite3, 1f);
        
        yield return new WaitForSecondsRealtime(attack2Delay);
        canAttack = true;
    }
}