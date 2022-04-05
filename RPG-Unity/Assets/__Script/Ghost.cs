using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Enemy  //A ghost is the same as a generic enemy besides it having a few different stats
{
    public override void Start()
    {
        base.Start();
        Speed = 3f;
        Radius = 5f;
        Experience = 5;
        Money = 50;
        EnemyName = "Ghost";
    }

    public override void Update()
    {
        base.Update();
        if (Shop.HasHelmet)
        {
            Radius = 2.5f;
        }
    }
}
