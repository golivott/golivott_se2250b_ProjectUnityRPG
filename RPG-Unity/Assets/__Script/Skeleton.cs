using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy   //A skeleton is similar to a generic enemy besides a few different stats and an extra attack
{
    private bool _throwBomb;
    public GameObject bomb;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        Health = 150;
        Speed = 5f;
        Power = 15;
        EnemyName = "Skeleton";
        _throwBomb = true;
    }

    public override void DetectedMovement()
    {
        base.DetectedMovement();
        if (_throwBomb)
        {
            var newBomb = Instantiate(bomb, GetEnemyCoordinates(), transform.rotation);
            _throwBomb = false;
            newBomb.GetComponent<Rigidbody2D>().velocity = VectorBetweenPlayerAndEnemy().normalized * 10f;
            Invoke("CanThrowBomb",3f);
        }
    }

    private void CanThrowBomb()
    {
        _throwBomb = true;
    }
}
