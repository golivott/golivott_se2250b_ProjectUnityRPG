using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerThree : Player
{
    // Properties for the 2 basic attacks
    public float attackDist = 1f;
    
    // Properties for attack 1
    [Header("Attack 1")]
    public float attack1Range = 2f;
    public float attack1Damage = 50f;
    public float attack1Delay = 1f;

    // Properties for attack 2
    [Header("Attack 2")]
    public float attack2Range = 2f;
    public float attack2Damage = 50f;
    public float attack2Delay = 1f;
    public float attack2Knockback = 1f;
    
    // Rage Ability
    [Header("Rage")] 
    public float duration = 5f;
    public float cooldown = 8f;
    public float maxStrengthBuff = 50f; // 50 strength is 100% damage increase
    public float maxResistanceDebuff = 0.5f; // multiplier is added to damage taken (0.5 here is 1.5*damage)
    
    private bool _canRage = true;
    
    // Axe throw ability
    [Header("Axe Throw")] 
    public float axeSpeed = 1f;
    public float axeSpin = 20f;
    public float axeDamage = 1f;
    public float axeCooldown = 1f;
    public GameObject axeSprite;

    private bool _canAxeThrow = true;
    
    public LayerMask enemyLayers;
    public GameObject attack1Sprite;
    public GameObject attack2Sprite;

    // Variables set by the script
    [Header("Set Dynamically")] 
    public Vector2 attack1Point;
    public Vector2 attack2Point;
    public bool canAttack = true;

    public override void Start()
    {
        base.Start();
        // Enemys are triggers so need to be able to interact with them
        Physics2D.queriesHitTriggers = true;
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
        // Updating attack point
        if (moveDir != Vector2.zero)
        {
            lastMoveDir = moveDir;
        }
        attack1Point = lastMoveDir * attackDist + new Vector2(transform.position.x, transform.position.y);
        attack2Point = lastMoveDir * attackDist * attack2Range / 2 +
                       new Vector2(transform.position.x, transform.position.y);
        
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
        
        // Rage on Q
        if (Input.GetKey(KeyCode.Q) && _canRage && unlockAbilityOne)
        {
            StartCoroutine(UseRage());
        }
        
        // Axe throw on E
        if (Input.GetKey(KeyCode.E) && _canAxeThrow && unlockAbilityTwo)
        {
            StartCoroutine(UseAxeThrow());
        }
    }
    
    // Attack 1
    private IEnumerator Attack1()
    {
        // Display animation
        GameObject attack1Sprite = Instantiate(this.attack1Sprite);
        attack1Sprite.transform.position = attack1Point;
        attack1Sprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
        Destroy(attack1Sprite, 0.1f);

        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attack1Point, attack1Range, enemyLayers);

        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attack1Damage);
        }

        // Attack cooldown
        yield return new WaitForSecondsRealtime(attack1Delay);
        canAttack = true;
    }

    private IEnumerator Attack2()
    {
        // Display animation
        GameObject attack2Sprite = Instantiate(this.attack2Sprite);
        attack2Sprite.transform.position = attack2Point;
        attack2Sprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
        Destroy(attack2Sprite, 0.1f);

        // Gets enemys hit by attack
        Collider2D[] enemyHits = Physics2D.OverlapCircleAll(attack2Point, attack2Range, enemyLayers);

        // Damages enemies
        foreach (Collider2D enemy in enemyHits)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attack2Damage);
            enemy.transform.position += new Vector3(lastMoveDir.x, lastMoveDir.y, 0).normalized * attack2Knockback;
        }

        // Attack cooldown
        yield return new WaitForSecondsRealtime(attack2Delay);
        canAttack = true;
    }

    private IEnumerator UseRage()
    {
        _canRage = false;
        // Activate Rage
        // Calculate player properties
        GetComponent<SpriteRenderer>().color = new Color(1, 0.2f, 0.2f);
        float strengthBuff = GetHealth() / 100f * maxStrengthBuff; // %health * max buff
        float resistanceDebuff = maxResistanceDebuff - GetHealth() / 100f * maxResistanceDebuff;// max debuff - %health * max debuff
        
        // Applying changes
        AddStrength(strengthBuff);
        AddDmgTakenMultiplier(resistanceDebuff);
        
        yield return new WaitForSecondsRealtime(duration);
        
        // Deactivate Rage
        GetComponent<SpriteRenderer>().color = new Color(1, 1f, 1f);
        
        // Remove changes
        AddStrength(-strengthBuff);
        AddDmgTakenMultiplier(-resistanceDebuff);

        yield return new WaitForSecondsRealtime(cooldown - duration);
        _canRage = true;
    }

    private IEnumerator UseAxeThrow()
    {
        _canAxeThrow = false;
        // Display animation
        GameObject axe = Instantiate(axeSprite);
        axe.transform.position = transform.position;
        axe.GetComponent<Rigidbody2D>().velocity = axeSpeed * lastMoveDir.normalized * Time.fixedDeltaTime;
        axe.GetComponent<ProjectileAttack>().SetSpin(axeSpin);

        // Set Damage
        axe.GetComponent<ProjectileAttack>().SetDamage(axeDamage);

        // Destroy after 0.5 sec
        Destroy(axe, 1f);

        // Wait delay before allowing another attack
        yield return new WaitForSecondsRealtime(axeCooldown);

        _canAxeThrow = true;
    }
}
