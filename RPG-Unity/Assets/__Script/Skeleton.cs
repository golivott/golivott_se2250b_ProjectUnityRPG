using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy   //A skeleton is similar to a generic enemy besides a few different stats and an extra attack
{
    //A skeleton can throw a bomb
    private bool _throwBomb;
    public GameObject bomb;
    
    public override void Start()    //calls the start from the base class and changes a few attributes
    {
        base.Start();   
        Health = 150;
        Speed = 5f;
        Power = 15;
        EnemyName = "Skeleton";
        _throwBomb = true;
    }

    public override void Update()
    {
        base.Update();
        if (Shop.HasHelmet)
        {
            Radius = 5;
        }
    }

    public override void DetectedMovement()     //overrides detected movement, enemy still locks on but can throw bombs towards player
    {
        base.DetectedMovement();
        if (_throwBomb)     //every 3 seconds a bomb can be thrown
        {
            var newBomb = Instantiate(bomb, GetEnemyCoordinates(), transform.rotation);
            _throwBomb = false;
            newBomb.GetComponent<Rigidbody2D>().velocity = VectorBetweenPlayerAndEnemy().normalized * 10f;
            Invoke("CanThrowBomb",3f);
        }
    }

    private void CanThrowBomb()     //method used to determine when a skeleton can or can't throw a bomb
    {
        _throwBomb = true;
    }
}
