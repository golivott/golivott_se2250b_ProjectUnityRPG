using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using UnityEngine;
using Random = UnityEngine.Random;
using Math = UnityEngine.Mathf;

public class Enemy : MonoBehaviour  //Generic enemy class
{
    //appropriate enemy attributes
    private float _health;
    private float _speed;
    private float _radius;
    private float _power;
    private int _experience;
    private string _enemyName;
    private int _money;
    private bool _moveEnemy;
    public bool doUndetectedMove = false;
    


    // Start is called before the first frame update
    public virtual void Start()     //assigns the generic enemy basic values
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
        if (_moveEnemy)     //if the enemy can move
        {
            if (DistancedBetweenPlayerAndEnemy() > _radius || doUndetectedMove)     //if the player isn't within the enemies radius the passive movement gets called
            {
                GetComponent<Collider2D>().isTrigger = false;
                _moveEnemy = false;
                Invoke("UnDetectedMovement", 1f);
            }
            else
            {
                DetectedMovement();     //if the player is within range the detected movement gets called
            }
        }

        if (_health <= 0)   //if the enemy has less than zero health it dies
        {
            KillEnemy();
        }
    }
    public void UnDetectedMovement()    //passive movement consists of an enemy moving for one second in a direction of a randomly generated 2D vector
    {
        _moveEnemy = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f,1f)) * _speed;
        Invoke("Stop", 1f);
    }

    public virtual void DetectedMovement()  //Method where the enemy locks onto the player
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = VectorBetweenPlayerAndEnemy().normalized * _speed;
    }
    
    protected GameObject GetEnemy()     //method that returns the enemy object
    {
        return gameObject;
    }

    public void Stop()  //method used for passive movement, stops the enemy every second 
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        _moveEnemy = true;
        GetComponent<Collider2D>().isTrigger = false;
    }

    public Vector2 GetPlayerCoordinates()   //method that gets the player coordinates
    {
        return new Vector2(GameObject.FindWithTag("Player").transform.position.x,
            GameObject.FindWithTag("Player").transform.position.y);
    }

    public Vector2 GetEnemyCoordinates()    //method that gets the enemy coordinates
    {
        return new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
    }
    
    public float DistancedBetweenPlayerAndEnemy()   //method that calculates the distance between the player and enemy (magnitude)
    {
        return Mathf.Sqrt(Mathf.Pow(GetPlayerCoordinates().x - GetEnemyCoordinates().x, 2) +
                          Math.Pow(GetPlayerCoordinates().y - GetEnemyCoordinates().y, 2));
    }

    public Vector2 VectorBetweenPlayerAndEnemy()    //method that calculates the directional vector between the player and enemy
    {
        return new Vector2(GetPlayerCoordinates().x - GetEnemyCoordinates().x, GetPlayerCoordinates().y - GetEnemyCoordinates().y);
    }

    public void KillEnemy()     //method that kills the enemy and adds money and experience to the player
    {
        GameObject.FindWithTag("Player").GetComponent<Player>().AddExperience(_experience);
        GameObject.FindWithTag("Player").GetComponent<Player>().AddMoney(_money);
        Destroy(gameObject);
        
        //Add experience to player and add money
        
    }

    public void TakeDamage(float damage)    //method used to make the enemy take damage when a player attacks
    {
        _health -= damage * (GameObject.FindWithTag("Player").GetComponent<Player>().GetStrength() * 2 / 100f + 1);
    }

    //Appropriate getters and setters
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
