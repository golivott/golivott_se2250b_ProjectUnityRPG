using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorExplosion : MonoBehaviour
{
    // Some variables to set how the projectile should move and act
    public float growth = 1.06f;

    private void FixedUpdate()
    {
        transform.localScale *= growth;
        Destroy(gameObject, 0.75f);
    }
}

