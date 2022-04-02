using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOne : Player
{
    // Properties for the 2 basic attacks
    // Properties for attack 1
    [Header("Attack 1")]
    public float attackDist = 1f;
    public float attack1Range = 1f;
    public float attack1Damage = 50f;
    public float attack1Delay = 0.5f;

    // Properties for attack 2
    [Header("Attack 2")]
    public float attack2Damage = 25f;
    public float attack2Delay = 1f;
    public float attack2Speed = 1f;
    
    // Variables for Sword Slash ability
    [Header("Sword Slash")]
    public float swordSlashDamage = 50f;
    public float swordSlashDelay = 1;
    public GameObject swordSlashSprite;
    public float swordSpinRate = 1f;
    private bool _canSwordSlash = true;

    // Variable for fire stomp ability
    [Header("Fire Stomp")]
    public float fireStompDamage = 100f;
    public float fireStompDelay = 1f;
    public float fireStopGrowth = 1f;
    public GameObject fireStompSprite;
    private bool _canFireStomp = true;
    
    public LayerMask enemyLayers;
    public GameObject attack1Sprite;
    public GameObject attack2Sprite;

    // Variables set by the script
    [Header("Set Dynamically")] 
    public Vector2 attack1Point;
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
        
        // Sword Slash on Q
        if (Input.GetKey(KeyCode.Q) && _canSwordSlash && unlockAbilityOne)
        {
            _canSwordSlash = false;
            StartCoroutine(UseSwordSlash());
        }

        // Fire Stomp on E
        if (Input.GetKey(KeyCode.E) && _canFireStomp && unlockAbilityTwo)
        {
            _canFireStomp = false;
            StartCoroutine(UseFireStomp());
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

        // attack cooldown
        yield return new WaitForSecondsRealtime(attack1Delay);
        canAttack = true;
    }

    // Attack 2
    private IEnumerator Attack2()
    {
        // Display animation
        GameObject attack2Sprite = Instantiate(this.attack2Sprite);
        attack2Sprite.transform.position = attack1Point;
        attack2Sprite.transform.rotation = Quaternion.EulerAngles(0, 0, Mathf.Atan2(lastMoveDir.y, lastMoveDir.x));
            
        // Setting proporties of attack
        attack2Sprite.GetComponent<Rigidbody2D>().velocity = lastMoveDir.normalized * attack2Speed * Time.fixedDeltaTime;
        attack2Sprite.GetComponent<ProjectileAttack>().SetDamage(attack2Damage);
            
        // Destroying attack
        Destroy(attack2Sprite, 0.3f);

        // 
        yield return new WaitForSecondsRealtime(attack2Delay);
        canAttack = true;
    }
    private IEnumerator UseSwordSlash()
    {
        // Display animation
        GameObject swordSlash = Instantiate(swordSlashSprite);
        swordSlash.transform.position = transform.position;
        swordSlash.GetComponent<ProjectileAttack>().SetSpin(swordSpinRate);
        swordSlash.GetComponent<ProjectileAttack>().IsFollowPlayer();

        // Set Damage
        swordSlash.GetComponent<ProjectileAttack>().SetDamage(swordSlashDamage);
        
        // Destroy after 0.5 sec
        Destroy(swordSlash, 0.5f);

        // Wait delay before allowing another attack
        yield return new WaitForSecondsRealtime(swordSlashDelay);
        _canSwordSlash = true;
    }

    private IEnumerator UseFireStomp()
    {
        // Display animation
        GameObject fireStomp = Instantiate(fireStompSprite);
        fireStomp.transform.position = transform.position;
        fireStomp.GetComponent<ProjectileAttack>().SetGrowth(fireStopGrowth);

        // Set Damage
        fireStomp.GetComponent<ProjectileAttack>().SetDamage(fireStompDamage);

        // Destroy after 0.5 sec
        Destroy(fireStomp, 1f);

        // Wait delay before allowing another attack
        yield return new WaitForSecondsRealtime(fireStompDelay);

        _canFireStomp = true;
    }
}
