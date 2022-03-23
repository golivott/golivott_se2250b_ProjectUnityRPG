using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour   //Script used to manage proper bomb collision
{
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other)  
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Enemy") && !other.CompareTag("Bomb"))     //if the bomb collides with a wall, destroy it
        {
            Destroy(gameObject);
        }
    }
}
