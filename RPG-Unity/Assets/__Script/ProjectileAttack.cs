using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    // Some variables to set how the projectile should move and act
    private float _damage = 50f;
    private float _spin = 0f;
    private bool _followPlayer = false;
    private float _growth = 0f;

    private void FixedUpdate()
    {
        // Apply rotation
        transform.Rotate(0,0,_spin);

        // Apply Growth
        if (_growth != 0)
        {
            transform.localScale *= _growth;
        }
        
        // if it should follow the player
        if (_followPlayer)
        {
            transform.position = GameObject.FindWithTag("Player").gameObject.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Apply damage to the enemy
        if (col.gameObject.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }

    // Sets damage value
    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    
    // Sets spin value
    public void SetSpin(float spin)
    {
        _spin = spin;
    }

    // Sets if the attack should follow the player
    public void IsFollowPlayer()
    {
        _followPlayer = true;
    }

    // Sets if the attack should grow in size
    public void SetGrowth(float growth)
    {
        _growth = growth;
    }
    
}
