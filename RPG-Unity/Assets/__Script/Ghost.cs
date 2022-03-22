using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy  //A ghost is very similar to the generic enemy besides a few different stats
{
    public override void Start()
    {
        base.Start();
        Speed = 3f;
        Radius = 5f;
        Experience = 5;
        Money = 5;
        EnemyName = "Ghost";
    }
}
