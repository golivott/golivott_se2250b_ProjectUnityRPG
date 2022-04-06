using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplosion : MonoBehaviour
{
    
    public float growth = 1.06f;    //growth factor for meteor explosion

    private void FixedUpdate()  //upon every physics frame, grow the game object by a factor of 1.06 and destroy it 3/4 of a second after instantiation
    {
        transform.localScale *= growth;
        Destroy(gameObject, 0.75f);
    }
}

