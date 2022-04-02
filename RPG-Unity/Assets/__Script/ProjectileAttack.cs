using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    // Some variables to set how the projectile should move and act
    public float damage = 50f;
    public float spin = 0f;
    public float growth = 0f;
    public float killAfter = 0f;
    public bool followPlayer = false;
    public bool destroyOnHit = false;
    
    public GameObject spawnOnDeath;

    private void FixedUpdate()
    {
        if (killAfter != 0f)
        {
            Destroy(gameObject, killAfter);
        }
        
        // Apply rotation
        transform.Rotate(0,0,spin);

        // Apply Growth
        if (growth != 0)
        {
            transform.localScale *= growth;
        }
        
        // if it should follow the player
        if (followPlayer)
        {
            transform.position = GameObject.FindWithTag("Player").gameObject.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Apply damage to the enemy
        if (col.gameObject.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);

            if (spawnOnDeath != null)
            {
                Instantiate(spawnOnDeath, transform.position, Quaternion.identity);
            }

            if (destroyOnHit)
            {
                Destroy(gameObject);
            }
        }
    }

    // Sets damage value
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    
    // Sets spin value
    public void SetSpin(float spin)
    {
        this.spin = spin;
    }

    // Sets if the attack should follow the player
    public void IsFollowPlayer()
    {
        followPlayer = true;
    }

    // Sets if the attack should grow in size
    public void SetGrowth(float growth)
    {
        this.growth = growth;
    }

    public void setDestroyOnHit(bool destroyOnHit)
    {
        this.destroyOnHit = destroyOnHit;
    }
    public void SetSpawnOnDeath(GameObject spawnOnDeath)
    {
        this.spawnOnDeath = spawnOnDeath;
    }
    
}
