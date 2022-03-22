using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int skillPoints;
    private float _health;
    private float _speed;
    private float _strength;
    private float _stamina;
    private float _resistance;
    private int _level;
    private int _experience;
    private int _money;
    private int _skillPoints;
    private bool _canTakeDamage;
    private Movement _movement;
    private Enemy _enemy;
    private bool _iFrames;
    
    public float interactDistance = 1f;
    public float interactRange = 1f;
    public LayerMask interactLayer;
    public Vector2 interactPoint;
    public Vector2 lastMoveDir;  

    // Start is called before the first frame update
    void Start()
    {
        skillPoints = 30;
        _health = 100f;
        _speed = 10f;
        _strength = 10f;
        _stamina = 100f;
        _resistance = 10f;
        _level = 0;
        _experience = 0;
        _money = 0;
        _skillPoints = 0;
        _canTakeDamage = true;
        _movement = gameObject.GetComponent<Movement>();
        _enemy = GameObject.FindWithTag("Enemy").GetComponent<Enemy>();
        _iFrames = false;
    }
    void Update()
    {
        if (GetComponent<Movement>().moveDir != Vector2.zero)
        {
            lastMoveDir = GetComponent<Movement>().moveDir;
            interactPoint = GetComponent<Movement>().moveDir * interactDistance + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKey(KeyCode.F))
        {
            Collider2D[] enemyHits = Physics2D.OverlapCircleAll(interactPoint, interactRange, interactLayer);
        }

        if (_health == 0)
        {
            print("Game Over");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        _movement.disableMovement = true;
        gameObject.GetComponent<Rigidbody2D>().velocity = _enemy.VectorBetweenPlayerAndEnemy().normalized * 6f;
        Invoke("EnableMovement",1f);

        if (_canTakeDamage)
        {

            if (collision.gameObject.CompareTag("Enemy"))
            {
                _health = _health - 10f;
                print(_health);
                _canTakeDamage = false;
                Invoke("SetCanTakeDamage", 2f);
                StartCoroutine(Frames());
            }
        }
    }



    public float GetHealth()
    {
        return _health;
    }
    public float GetSpeed()
    {
        return _speed;
    }
    public float GetStrength()
    {
        return _strength;
    }
    public float GetStamina()
    {
        return _stamina;
    }
    public float GetResistance()
    {
        return _resistance;
    }
    public int GetLevel()
    {
        return _level;
    }
    public int GetExperience()
    {
        return _experience;
    }
    public int GetMoney()
    {
        return _money;
    }
    public int GetSkillPoints()
    {
        return _skillPoints;
    }
    public void AddHealth(float health)
    {
        _health = _health + health;
    }
    public void AddSpeed(float speed)
    {
        _speed = _speed + speed;
    }
    public void AddStrength(float strength)
    {
        _strength = _strength + strength;
    }
    public void AddStamina(float stamina)
    {
        _stamina = _stamina + stamina;
    }
    public void AddResistance(float resistance)
    {
        _resistance = _resistance + resistance;
    }
    public void AddLevel(int level)
    {
        _level = level + _level;
    }
    public void AddExperience(int experience)
    {
        _experience = _experience + experience;
    }
    public void AddMoney(int money)
    {
        _money = _money + money;
    }
    public void AddSkillPoints(int skillPoints)
    {
        _skillPoints = _skillPoints + skillPoints;
    }

    public void SetCanTakeDamage()
    {
        _canTakeDamage = true;
    }

        public void SetCannotTakeDamage()
    {
        _canTakeDamage = false;
    }


    public void EnableMovement()
    {
        _movement.disableMovement = false;
    }

    public IEnumerator Frames()
    {
        for (int c = 0; c < 7; c++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }
}
