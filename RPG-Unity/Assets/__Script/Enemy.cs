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
    private float _power;
    private int _experience;
    private string _enemyName;
    private int _money;
    private bool _moveEnemy;


    // Start is called before the first frame update
    public virtual void Start()
    {
        _health = 100f;
        _speed = 4f;
        _radius = 10f;
        _power = 10f;
        _experience = 10;
        _enemyName = "Default Enemy";
        _money = 10;
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        _moveEnemy = true;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
        if (_moveEnemy)
        {
            if (DistancedBetweenPlayerAndEnemy() > _radius)
            {
                GetComponent<Collider2D>().isTrigger = false;
                _moveEnemy = false;
                Invoke("UnDetectedMovement", 1f);
            }
            else
            {
                DetectedMovement();
            }
        }

        if (_health <= 0)
        {
            //Play death animation then despawn enemy after 5 seconds
            KillEnemy();
        }
    }
    public void UnDetectedMovement()
    {
        _moveEnemy = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)) * _speed;
        Invoke("Stop", 1f);
    }

    public virtual void DetectedMovement()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = VectorBetweenPlayerAndEnemy().normalized * _speed;
    }
    
    protected GameObject GetEnemy()
    {
        return gameObject;
    }

    public void Stop()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _moveEnemy = true;
        GetComponent<Collider2D>().isTrigger = false;
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

    public float DistancedBetweenPlayerAndEnemy()
    {
        return Mathf.Sqrt(Mathf.Pow(GetPlayerCoordinates().x - GetEnemyCoordinates().x, 2) +
                          Math.Pow(GetPlayerCoordinates().y - GetEnemyCoordinates().y, 2));
    }

    public Vector2 VectorBetweenPlayerAndEnemy()
    {
        return new Vector2(GetPlayerCoordinates().x - GetEnemyCoordinates().x, GetPlayerCoordinates().y - GetEnemyCoordinates().y);
    }

    public void KillEnemy()
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().AddExperience(_experience);
        GameObject.FindWithTag("Player").GetComponent<Player>().AddMoney(_money);
        Destroy(gameObject);
        
        //Add experience to player and add money
        
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
    }

    public float Health
    {
        get => _health;
        set => _health = value;
    }

    public float Speed
    {
        get => _speed;
        set => _speed = value;
    }

    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    public int Experience
    {
        get => _experience;
        set => _experience = value;
    }

    public string EnemyName
    {
        get => _enemyName;
        set => _enemyName = value;
    }

    public int Money
    {
        get => _money;
        set => _money = value;
    }
    public bool MoveEnemy
    {
        get => _moveEnemy;
        set => _moveEnemy = value;
    }

    public float Power
    {
        get => _power;
        set => _power = value;
    }
}
