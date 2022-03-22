using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAttack : MonoBehaviour
{
    private List<Collider2D> _colliders;
    private float _damage = 50f;
    private float _spin = 0f;
    private bool _followPlayer = false;
    private float _growth = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        _colliders = new List<Collider2D>(); 
    }

    private void FixedUpdate()
    {
        transform.Rotate(0,0,_spin);

        if (_growth != 0)
        {
            transform.localScale *= _growth;
        }

        if (_followPlayer)
        {
            transform.position = GameObject.FindWithTag("Player").gameObject.transform.position;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag.Equals("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().TakeDamage(_damage);
        }
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }
    
    public void SetSpin(float spin)
    {
        _spin = spin;
    }

    public void IsFollowPlayer()
    {
        _followPlayer = true;
    }

    public void SetGrowth(float growth)
    {
        _growth = growth;
    }
    
}
