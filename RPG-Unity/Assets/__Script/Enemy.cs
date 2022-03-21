using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Math = UnityEngine.Mathf;

public class Enemy : MonoBehaviour  //Enemy super class
{
    private float _health;
    private float _speed;
    private float _radius;
    private float _experience;
    private string _enemyName;
    private int _money;
    private Rigidbody2D _rb;
    private bool _moveEnemy;


    // Start is called before the first frame update
    void Start()
    {
        _health = 100f;
        _speed = 4f;
        _radius = 10f;
        _experience = 10f;
        _enemyName = "Default Enemy";
        _money = 10;
        _rb = gameObject.AddComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _moveEnemy = true;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Mathf.Sqrt(Mathf.Pow(GetPlayerCoordinates().x - GetEnemyCoordinates().x, 2) +
                                    Math.Pow(GetPlayerCoordinates().y - GetEnemyCoordinates().y, 2));
        print(_moveEnemy);
        if (_moveEnemy)
        {
            if (distance > _radius)
            {
                _moveEnemy = false;
                Invoke("UnDetectedMovement", 3f);
            }
            else
            {
                DetectedMovement();
            }
        }

        if (_health == 0)
        {
            //Play death animation then despawn enemy after 5 seconds
            Invoke("KillEnemy", 5f);
        }
    }

    public void OnTriggerEnter2D(Collider other)
    {
        //Subtract 10 health from player and grant 3 seconds of iFrames to the player
    }

    public void UnDetectedMovement()
    {
        _moveEnemy = true;
        _rb.velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)) * _speed;
        Invoke("Stop", 3f);
    }

    public void DetectedMovement()
    {
        _rb.velocity = VectorBetweenPlayerAndEnemy().normalized * _speed;
    }
    
    protected GameObject GetEnemy()
    {
        return gameObject;
    }

    public void Stop()
    {
        _rb.velocity = Vector2.zero;
        _moveEnemy = true;
    }

    public Vector2 GetPlayerCoordinates()
    {
        return new Vector2(GameObject.FindWithTag("Player").transform.position.x,
            GameObject.FindWithTag("Player").transform.position.y);
    }

    public Vector2 GetEnemyCoordinates()
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }

    public Vector2 VectorBetweenPlayerAndEnemy()
    {
        return new Vector2(GetPlayerCoordinates().x - GetEnemyCoordinates().x, GetPlayerCoordinates().y - GetEnemyCoordinates().y);
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
        
        //Add experience to player and add money
    }
}
