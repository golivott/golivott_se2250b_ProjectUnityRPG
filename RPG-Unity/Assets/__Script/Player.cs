using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Scene = UnityEditor.SearchService.Scene;

public class Player : MonoBehaviour
{
    //all appropriate attributes for a player
    private int _health;
    private float _speed;
    private float _strength;
    private float _dmgTakenMultiplier;
    private int _level;
    private int _experience;
    private int _money;
    private int _skillPoints;

    private bool _canTakeDamage;
    private bool _iFrames;
    private Interaction _interaction;

    [Header("Attack 1")]
    public float attack1Damage = 50f;
    public float attack1Delay = 0.5f;

    // Properties for attack 2
    [Header("Attack 2")]
    public float attack2Damage = 25f;
    public float attack2Delay = 1f;
    
    [Header("Ability 1")]
    public float ability1Damage = 25f;
    public float ability1Delay = 1f;
    
    [Header("Ability 2")]
    public float ability2Damage = 25f;
    public float ability2Delay = 1f;
    
    [Header("Interactions")]
    public float interactDistance = 1f;
    public float interactRange = 1f;
    public LayerMask interactLayer;
    public Vector2 interactPoint;

    [Header("UI")]
    public UIHealthBar healthBar;
    public ExperienceBar expBar;
    public Image levelTwoIcon;

    [Header("Movement Settings")]
    public Vector2 lastMoveDir;
    public Vector2 moveDir;
    public Animator animator;
    public bool disableMovement;
    public float dashSpeed;
    public float dashLength = 0.2f;
    public float dashCooldown = 1f;

    [Header("Manage Abilities")]
    public bool unlockAbilityOne = false;
    public bool unlockAbilityTwo = false;

    [Header("SkillTree UI")]
    public GameObject skillTreeUI; //Skilltree UI
    
    [Header("Shop UI")]
    public GameObject shopUI; //shop UI

    private float _activeMoveSpeed;
    private float _dashCounter;
    private float _dashCoolCounter;

    private bool _canRegen = true;
    private bool _canBeInvisible = true;
    
    public virtual void Start()     //assigns attributes a value for a generic player
    {
        _skillPoints = 0;
        _health = 100;
        _speed = 350f;
        _strength = 1f;
        _dmgTakenMultiplier = 1f;
        _level = 1;
        _experience = 0;
        _money = 5000;
        _canTakeDamage = true;
        _iFrames = false;
        _interaction = gameObject.GetComponent<Interaction>();
        _activeMoveSpeed = _speed;
        skillTreeUI.SetActive(false);
        healthBar.SetMaxHealth(_health);
        expBar.SetMaxExperience(25);
        expBar.ResetExperience();
        levelTwoIcon.enabled = false;
        attack1Damage = 50;
        attack1Delay = 0.5f;
        attack2Damage = 25;
        attack2Delay = 1f;
    }
    public virtual void Update()
    {
        if (Shop.HasRubyRing && _canRegen)
        {
            _canRegen = false;
            Invoke(nameof(Regeneration), 2.5f);
        }

        GameObject.Find("Health").GetComponent<Text>().text = "Health: " + _health;
        GameObject.Find("Level").GetComponent<Text>().text = "Player Level: " + _level;
        GameObject.Find("Experience").GetComponent<Text>().text = "Exp: " + _experience;
        expBar.SetExperience(_experience);
        
        if (moveDir != Vector2.zero)   //if statement that creates an interact range for the player
        {
            lastMoveDir = moveDir;
            interactPoint = moveDir * interactDistance + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKey(KeyCode.F))    //if the player clicks F, they can interact with objects that are interactable
        {
            Physics2D.queriesHitTriggers = true;
            Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(interactPoint, interactRange, interactLayer);     //gets all colliders with layers that are interactable
            if (interactableObjects.Length != 0){    //if an interacatable object was found call the interact method with that gameObject passed in
                _interaction.Interact(interactableObjects[0].gameObject);
            }
        }

        if (_experience >= 25 * _level)   //if the players XP reaches a value greater than or equal to 50 they level up and experience is reset to 0
        {
            _level++;
            _experience = _experience - 25;
            _skillPoints+=10;
            expBar.SetMaxExperience(_level*25);
        }

        if (_health <=  0)   //if the players health reaches 0, the game restarts to the current level the user is on
        {
            Destroy(gameObject);
            SceneManager.LoadScene("StartingMenu");
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) //Toggles overlay
        {
            skillTreeUI.SetActive(!skillTreeUI.activeSelf);
        }
        
        if (Input.GetKeyDown(KeyCode.P)) //Toggles overlay
        {
            shopUI.SetActive(!shopUI.activeSelf);
        }
        
        
        
    }

    public virtual void FixedUpdate()   //method for player movement and dash movement
    {
        if (!disableMovement)
        {
            // Calculating move direction
            moveDir = Vector2.zero;

            if (Input.GetKey(KeyCode.W))
                moveDir.y = 1;
            if (Input.GetKey(KeyCode.S))
                moveDir.y = -1;
            if (Input.GetKey(KeyCode.D))
                moveDir.x = 1;
            if (Input.GetKey(KeyCode.A))
                moveDir.x = -1;

            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
            animator.SetFloat("Magnitude", moveDir.magnitude);


            if (Input.GetKey(KeyCode.Space))
            {
                if (_dashCoolCounter <= 0 && _dashCounter <= 0)
                {
                    _activeMoveSpeed = dashSpeed + _activeMoveSpeed;
                    _dashCounter = dashLength;
                    SetCannotTakeDamage();
                }
            }

            if (_dashCounter > 0)
            {
                _dashCounter -= Time.fixedDeltaTime;

                if (_dashCounter <= 0)
                {
                    _activeMoveSpeed = _speed;
                    _dashCoolCounter = dashCooldown;
                    SetCanTakeDamage();
                }
            }

            if (_dashCoolCounter > 0)
            {
                _dashCoolCounter -= Time.fixedDeltaTime;
            }

            // Moving Character
            gameObject.GetComponent<Rigidbody2D>().velocity =
                moveDir.normalized * _activeMoveSpeed * Time.fixedDeltaTime;
        }
    }
    
    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))    //if the player collides with an enemy they take 10 damage
        {
            TakenDamage(10);
            healthBar.SetHealth(_health);
        }

        else if (collision.gameObject.CompareTag("Bomb"))   //if the player collides with a bomb they take 20 damage
        {
            TakenDamage(20);
            healthBar.SetHealth(_health);
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
    public float GetActiveMoveSpeed()
    {
        return _activeMoveSpeed;
    }
    
    public float GetSpeed()
    {
        return _speed;
    }
    
    public float GetStrength()
    {
        return _strength;
    }
    public float GetResistance()
    {
        return _dmgTakenMultiplier;
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
    public void AddHealth(int health)
    {
        _health = _health + health;
    }
    public void AddSpeed(float speed)
    {
        _activeMoveSpeed = _activeMoveSpeed + speed;
    }
    public void AddStrength(float strength)
    {
        _strength = _strength + strength;
    }
    public void AddDmgTakenMultiplier(float dmgTakenMultiplier)
    {
        _dmgTakenMultiplier = _dmgTakenMultiplier + dmgTakenMultiplier;
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

    public void SetActiveMoveSpeed(float speed)
    {
        _activeMoveSpeed = speed;
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public void SetResistance(float resistance)
    {
        _dmgTakenMultiplier = resistance;
    }

    public void SetStrength(float strength)
    {
        _strength = strength;
    }
    
    
    
    public void TakenDamage(int damage) //method used to calculate the player takes from an enemy
        {
            disableMovement = true;   //when the player gets hit there movement gets disabled for a short while since they take knockback
            gameObject.GetComponent<Rigidbody2D>().velocity = GameObject.FindWithTag("Enemy").GetComponent<Enemy>().VectorBetweenPlayerAndEnemy().normalized * 6f;  //makes player take knockback in direction they get hit
            Invoke("EnableMovement", 1f);   //After one second, the knockback is over and the player can move

            if (_canTakeDamage)     //if the player doesn't have IFrames they can take damage
            {
                   
                _health -=  Mathf.RoundToInt(damage * _dmgTakenMultiplier); //subtract the damage from health

                _canTakeDamage = false; //give player iframes
                Invoke("SetCanTakeDamage", 2f); //After two seconds the player can take damage again
                StartCoroutine(Frames());   //ques Iframes
            }
        }


    public void EnableMovement()    //enables player movement
    {
        disableMovement = false;
    }

    public IEnumerator Frames() //repeatly changes the game objects color to simulate iframes
    {
        for (int c = 0; c < 7; c++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            yield return new WaitForSecondsRealtime(0.1f);
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
        
        if (Shop.HasNecklace && (_health <= Mathf.RoundToInt(100 * 0.1f)) && _canBeInvisible)
        {
            StartCoroutine(UseInvisability());
        }
    }
    public void Regeneration()
    {
        if (_health != 100)
        {
            _health++;
        }
        _canRegen = true;
    }

    private IEnumerator UseInvisability()
    {
        _canBeInvisible = false;
        Invoke(nameof(SetCanBeInvisible), 180);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Make player invisible
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        // Forces all enemies to do undetected movement
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().doUndetectedMove = true;
        }

        yield return new WaitForSecondsRealtime(30);

        // Make player visible again
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        // disables forcing of enemy undetected movement
        foreach (GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().doUndetectedMove = false;
        }
    }
    public void SetCanBeInvisible()
    {
        _canBeInvisible = true;
    }
}
