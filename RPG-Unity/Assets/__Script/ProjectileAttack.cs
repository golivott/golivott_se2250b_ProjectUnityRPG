using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    private List<Collider2D> _colliders;
    private float damage = 50f;
    // Start is called before the first frame update
    void Start()
    {
        _colliders = new List<Collider2D>(); 
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        print("hit: " + col.gameObject.name);
        if (col.gameObject.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    
}
