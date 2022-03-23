using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //all appropriate attributes for a player
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
    private bool _iFrames;
    private Interaction _interaction;
    private Player _player;

    public float interactDistance = 1f;
    public float interactRange = 1f;
    public LayerMask interactLayer;
    public Vector2 interactPoint;
    public Vector2 lastMoveDir;
    

    // Start is called before the first frame update
    public void Start()     //assigns attributes a value for a generic player
    {
        _skillPoints = 0;
        _health = 100f;
        _speed = 10f;
        _strength = 10f;
        _stamina = 100f;
        _resistance = 10f;
        _level = 1;
        _experience = 0;
        _money = 0;
        _canTakeDamage = true;
        _movement = gameObject.GetComponent<Movement>();
        _iFrames = false;
        _interaction = gameObject.GetComponent<Interaction>();
        
    }
    public void Update()
    {
        GameObject.Find("Health").GetComponent<Text>().text = "Health: " + _health;
        GameObject.Find("Level").GetComponent<Text>().text = "Level: " + _level;
        GameObject.Find("Experience").GetComponent<Text>().text = "Exp: " + _experience;
        
        
        if (GetComponent<Movement>().moveDir != Vector2.zero)   //if statement that creates an interact range for the player
        {
            lastMoveDir = GetComponent<Movement>().moveDir;
            interactPoint = GetComponent<Movement>().moveDir * interactDistance + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKey(KeyCode.F))    //if the player clicks F, they can interact with objects that are interactable
        {
            Physics2D.queriesHitTriggers = true;
            Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(interactPoint, interactRange, interactLayer);     //gets all colliders with layers that are interactable
            if (interactableObjects.Length != 0)    //if an interacatable object was found call the interact method with that gameObject passed in
            {
                _interaction.Interact(interactableObjects[0].gameObject);
            }
        }
        
        if (_experience >= 25 * _level)   //if the players XP reaches a value greater than or equal to 50 they level up and experience is reset to 0
        {
            _level++;
            _experience = _experience - 25;
            _skillPoints+=10;
        }

        if (_health <= 0)   //if the players health reaches 0, the game restarts
        {
            SceneManager.LoadScene("Level1");
        }


    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))    //if the player collides with an enemy they take 10 damage
        {
            TakenDamage(10);
        }
        
        else if (collision.gameObject.CompareTag("Bomb"))   //if the player collides with a bomb they take 20 damage
        {
            TakenDamage(20);
        }
        else if (collision.gameObject.CompareTag("Attic"))  //if a player collides with the stairs they get teleported to the attic
        {
            gameObject.transform.position = new Vector3(22, -5, 0);
        }
        else if (collision.gameObject.CompareTag("Downstairs")) //if a player collides with the stairs in the attic they get teleported to the mainfloor
        {
            gameObject.transform.position = new Vector3(-6.5f, -4.5f, 0);
        }
        else if (collision.gameObject.CompareTag("UpStairs"))   //if the player collides with the basement stairs they get teleported to the mainfloor
        {
            gameObject.transform.position = new Vector3(-19, -5, 0);
        }

    }
    
    //Getters and Adders for each attribute in the player class
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

    public void SetCanTakeDamage()  //method used for invincibilty frames
    {
        _canTakeDamage = true;
    }

        public void SetCannotTakeDamage()   //method used for invincibilty frames
    {
        _canTakeDamage = false;
    }

        public void TakenDamage(int damage) //method used to calculate the player takes from an enemy
        {
            _movement.disableMovement = true;   //when the player gets hit there movement gets disabled for a short while since they take knockback
            gameObject.GetComponent<Rigidbody2D>().velocity = GameObject.FindWithTag("Enemy").GetComponent<Enemy>().VectorBetweenPlayerAndEnemy().normalized * 6f;  //makes player take knockback in direction they get hit
            Invoke("EnableMovement", 1f);   //After one second, the knockback is over and the player can move

            if (_canTakeDamage)     //if the player doesn't have IFrames they can take damage
            {
                _health = _health - damage; //subtract the damage from health
                
                _canTakeDamage = false; //give player iframes
                Invoke("SetCanTakeDamage", 2f); //After two seconds the player can take damage again
                StartCoroutine(Frames());   //ques Iframes
            }
        }


    public void EnableMovement()    //enables player movement
    {
        _movement.disableMovement = false;
    }

    public IEnumerator Frames() //repeatly changes the game objects color to simulate iframex
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