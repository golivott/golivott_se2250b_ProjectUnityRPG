using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Scene = UnityEditor.SearchService.Scene;

public class Player : MonoBehaviour
{
    //all appropriate attributes for a player
    public int health;
    public float speed;
    public float strength;
    public float dmgTakenMultiplier;
    public int level;
    public int experience;
    public int money;
    public int skillPoints;

    private bool _canTakeDamage;
    private Interaction _interaction;

    public bool isInvisable = false;

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

    [Header("Pause Menu")] 
    public GameObject pauseMenu; //pause menu
    
    [Header("Item Images")]
    public Texture healthPotion;
    public Texture speedPotion;
    public Texture strengthPotion;
    public Texture resistancePotion;

    private float _activeMoveSpeed;
    private float _dashCounter;
    private float _dashCoolCounter;

    private bool _canRegen = true;
    private bool _canBeInvisible = true;

    public Dictionary<string, int> Inventory;

    public int differentItems;
    public List<string> itemList;

    private bool _isInvetoryOpen;
    private int _lastItemIndex;

    private bool _canDash;

    private bool _canUseHealthPotion;
    private bool _canUseSpeedPotion;
    private bool _canUseStrengthPotion;
    private bool _canUseResistancePotion;

    public virtual void Start()     //assigns attributes a value for a generic player
    {
        skillPoints = 0;
        health = 100;
        speed = 350f;
        strength = 1f;
        dmgTakenMultiplier = 1f;
        level = 1;
        experience = 0;
        money = 0;
        _canTakeDamage = true;
        _interaction = gameObject.GetComponent<Interaction>();
        _activeMoveSpeed = speed;
        skillTreeUI.SetActive(false);
        healthBar.SetMaxHealth(health);
        expBar.SetMaxExperience(25);
        expBar.ResetExperience();
        levelTwoIcon.color = new Color(1,1,1,0);
        attack1Damage = 50;
        attack1Delay = 0.5f;
        attack2Damage = 25;
        attack2Delay = 1f;
        Inventory = new Dictionary<string, int>();
        itemList = new List<string>();
        _canDash = true;
        _canUseHealthPotion = true;
        _canUseSpeedPotion = true;
        _canUseStrengthPotion = true;
        _canUseResistancePotion = true;
    }
    public virtual void Update()
    {
        GameObject.Find("MoneyText").GetComponent<Text>().text ="Money: " + "$" + money;
        if (Shop.HasRubyRing && _canRegen)
        {
            _canRegen = false;
            Invoke(nameof(Regeneration), 2.5f);
        }

        GameObject.Find("Health").GetComponent<Text>().text = "Health: " + health;
        GameObject.Find("Level").GetComponent<Text>().text = "Player Level: " + level;
        GameObject.Find("Experience").GetComponent<Text>().text = "Exp: " + experience;
        expBar.SetExperience(experience);

        if (moveDir != Vector2.zero)   //if statement that creates an interact range for the player
        {
            lastMoveDir = moveDir;
            interactPoint = moveDir * interactDistance + new Vector2(transform.position.x, transform.position.y);
        }

        if (Input.GetKeyDown(KeyCode.F))    //if the player clicks F, they can interact with objects that are interactable
        {
            Physics2D.queriesHitTriggers = true;
            Collider2D[] interactableObjects = Physics2D.OverlapCircleAll(interactPoint, interactRange, interactLayer);     //gets all colliders with layers that are interactable
            if (interactableObjects.Length != 0){    //if an interacatable object was found call the interact method with that gameObject passed in
                _interaction.Interact(interactableObjects[0].gameObject);
            }
        }

        if (experience >= 25 * level)   //if the players XP reaches a value greater than or equal to 50 they level up and experience is reset to 0
        {
            level++;
            experience = experience - 25;
            skillPoints+=10;
            expBar.SetMaxExperience(level*25);
        }

        if (health <=  0)   //if the players health reaches 0, the game restarts to the current level the user is on
        {
            Destroy(gameObject);
            SceneManager.LoadScene("StartingMenu");
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !shopUI.activeSelf && !pauseMenu.activeSelf) //Toggles overlay
        {
            skillTreeUI.SetActive(!skillTreeUI.activeSelf);
            if (skillTreeUI.activeSelf)
            {
                disableMovement = true;
            }
            else
            {
                disableMovement = false;
            }
        }
        
        

        if (Input.GetKeyDown(KeyCode.LeftControl) && !shopUI.activeSelf && !skillTreeUI.activeSelf) //Toggles overlay
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            if (pauseMenu.activeSelf)
            {
                disableMovement = true;
            }
            else
            {
                disableMovement = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!_isInvetoryOpen)
            {

                    _isInvetoryOpen = true;
                    SetItemImage();
            }
            else
            {
                _isInvetoryOpen = false;
                SetItemImage();
            }
        }

        if (_isInvetoryOpen)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_lastItemIndex == differentItems-1)
                {
                    _lastItemIndex = 0;
                }
                else{
                    _lastItemIndex++;
                }
                SetItemImage();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (_lastItemIndex == 0)
                {
                    _lastItemIndex = differentItems-1;
                }
                else{
                    _lastItemIndex--;
                }
                SetItemImage();
            }

            if (Input.GetKeyDown(KeyCode.V))
            {
                string currentitem = itemList.ElementAt(_lastItemIndex);

                if (currentitem.Equals("HealthPotion") && _canUseHealthPotion)
                {
                    StartCoroutine(HealthPotion());
                }

                else if (currentitem.Equals("SpeedPotion") && _canUseSpeedPotion)
                {
                    StartCoroutine(SpeedPotion());
                }

                else if (currentitem.Equals("StrengthPotion") && _canUseStrengthPotion)
                {
                    StartCoroutine(StrengthPotion());
                }

                else if (currentitem.Equals("ResistancePotion") && _canUseResistancePotion)
                {
                    StartCoroutine(ResistancePotion());
                }
                else
                {
                    goto setImage;
                }
                Inventory[currentitem]--;
                if (Inventory[currentitem] == 0)
                {
                    Inventory.Remove(currentitem);
                    itemList.Remove(currentitem);
                    differentItems--;
                    if (_lastItemIndex > itemList.Count - 1)
                    {
                        _lastItemIndex = 0;
                    }
                }
                setImage:
                SetItemImage();
            }
        }
    }

    public virtual void FixedUpdate() //method for player movement and dash movement
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

            if (Input.GetKey(KeyCode.Space) && _canDash)
            {
                StartCoroutine(Dash());
            }
            gameObject.GetComponent<Rigidbody2D>().velocity =
                moveDir.normalized * speed * Time.fixedDeltaTime;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        OnTriggerStay2D(collision.collider);
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))    //if the player collides with an enemy they take 10 damage
        {
            TakenDamage(10);
            healthBar.SetHealth(health);
        }

        else if (collision.gameObject.CompareTag("Bomb"))   //if the player collides with a bomb they take 20 damage
        {
            TakenDamage(20);
            healthBar.SetHealth(health);
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
        return health;
    }
    public float GetActiveMoveSpeed()
    {
        return _activeMoveSpeed;
    }
    public float GetSpeed()
    {
        return speed;
    }

    public float GetStrength()
    {
        return strength;
    }
    public float GetResistance()
    {
        return dmgTakenMultiplier;
    }
    public int GetLevel()
    {
        return level;
    }
    public int GetExperience()
    {
        return experience;
    }
    public int GetMoney()
    {
        return money;
    }
    public int GetSkillPoints()
    {
        return skillPoints;
    }
    public void AddHealth(int health)
    {
        this.health = this.health + health;
    }
    public void AddSpeed(float speed)
    {
        _activeMoveSpeed = _activeMoveSpeed + speed;
    }
    public void AddStrength(float strength)
    {
        this.strength = this.strength + strength;
    }
    public void AddDmgTakenMultiplier(float dmgTakenMultiplier)
    {
        this.dmgTakenMultiplier = this.dmgTakenMultiplier + dmgTakenMultiplier;
    }
    public void AddLevel(int level)
    {
        this.level = level + this.level;
    }
    public void AddExperience(int experience)
    {
        this.experience = this.experience + experience;
    }
    public void AddMoney(int money)
    {
        this.money = this.money + money;
    }
    public void AddSkillPoints(int skillPoints)
    {
        this.skillPoints = this.skillPoints + skillPoints;
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
        this.speed = speed;
    }

    public void SetResistance(float resistance)
    {
        dmgTakenMultiplier = resistance;
    }

    public void SetStrength(float strength)
    {
        this.strength = strength;
    }
    public void TakenDamage(int damage) //method used to calculate the player takes from an enemy
        {
            disableMovement = true;   //when the player gets hit there movement gets disabled for a short while since they take knockback
            gameObject.GetComponent<Rigidbody2D>().velocity = GameObject.FindWithTag("Enemy").GetComponent<Enemy>().VectorBetweenPlayerAndEnemy().normalized * 6f;  //makes player take knockback in direction they get hit
            Invoke("EnableMovement", 1f);   //After one second, the knockback is over and the player can move

            if (_canTakeDamage)     //if the player doesn't have IFrames they can take damage
            {

                health -=  Mathf.RoundToInt(damage * dmgTakenMultiplier); //subtract the damage from health

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

        if (Shop.HasNecklace && (health <= Mathf.RoundToInt(100 * 0.1f)) && _canBeInvisible)
        {
            StartCoroutine(UseInvisability());
        }

        if (isInvisable)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        }
    }

    public IEnumerator HealthPotion()
    {
        _canUseHealthPotion = false;
        for (int c = 0; c < 100; c++)
        {
            yield return new WaitForSecondsRealtime(0.6f);
            if (health != 100)
            {
                healthBar.SetHealth(health);
                health++;
            }
        }

        _canUseHealthPotion = true;
    }

    public IEnumerator SpeedPotion()
    {
        speed = speed * 2;
        _canUseSpeedPotion = false;
        yield return new WaitForSecondsRealtime(60f);
        speed = speed / 2;
        _canUseSpeedPotion = true;
    }

    public IEnumerator StrengthPotion() //repeatly changes the game objects color to simulate iframes
    {
        strength = strength + 50;
        _canUseStrengthPotion = false;
        yield return new WaitForSecondsRealtime(60f);
        strength = strength - 50;
        _canUseStrengthPotion = true;
    }
    public IEnumerator ResistancePotion() //repeatly changes the game objects color to simulate iframes
    {
        dmgTakenMultiplier = dmgTakenMultiplier / 2;
        _canUseResistancePotion = false;
        yield return new WaitForSecondsRealtime(60f);
        dmgTakenMultiplier = dmgTakenMultiplier / 2;
        _canUseResistancePotion = true;
    }

    public void Regeneration()
    {
        if (health != 100)
        {
            health++;
            healthBar.SetHealth(health);
        }
        _canRegen = true;
    }

    public IEnumerator Dash()
    {
        speed = speed * 3f;
        _canDash = false;
        yield return new WaitForSecondsRealtime(0.1f);
        speed = speed / 3f;
        yield return new WaitForSecondsRealtime(1f);
        _canDash = true;
    }

    private IEnumerator UseInvisability()
    {
        _canBeInvisible = false;
        Invoke(nameof(SetCanBeInvisible), 180);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // Make player invisible
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.4f);
        isInvisable = true;

        // Forces all enemies to do undetected movement
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().doUndetectedMove = true;
            }
        }

        yield return new WaitForSecondsRealtime(30);

        // Make player visible again
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        isInvisable = false;
        // disables forcing of enemy undetected movement
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().doUndetectedMove = false;
            }
        }
    }
    public void SetCanBeInvisible()
    {
        _canBeInvisible = true;
    }

    public GameObject GetShopUI()
    {
        return shopUI;
    }

    public GameObject GetSkillTreeUI()
    {
        return skillTreeUI;
    }

    private void SetItemImage()
    {
        if (_isInvetoryOpen)
        {
            if (itemList.Count != 0)
            {
                string firstItem = itemList.ElementAt(_lastItemIndex);
                if (firstItem.Equals("HealthPotion"))
                {
                    GameObject.Find("ItemImage").GetComponent<RawImage>().texture = healthPotion;
                    GameObject.Find("ItemAmount").GetComponent<Text>().text = "X " + Inventory["HealthPotion"];
                }
                else if (firstItem.Equals("SpeedPotion"))
                {
                    GameObject.Find("ItemImage").GetComponent<RawImage>().texture = speedPotion;
                    GameObject.Find("ItemAmount").GetComponent<Text>().text = "X " + Inventory["SpeedPotion"];
                }
                else if (firstItem.Equals("StrengthPotion"))
                {
                    GameObject.Find("ItemImage").GetComponent<RawImage>().texture = strengthPotion;
                    GameObject.Find("ItemAmount").GetComponent<Text>().text = "X " + Inventory["StrengthPotion"];
                }
                else
                {
                    GameObject.Find("ItemImage").GetComponent<RawImage>().texture = resistancePotion;
                    GameObject.Find("ItemAmount").GetComponent<Text>().text = "X " + Inventory["ResistancePotion"];
                }

                GameObject.Find("ItemImage").GetComponent<RawImage>().color = new Color(1, 1, 1, 1);
                GameObject.Find("ItemAmount").GetComponent<Text>().color = new Color(1, 1, 1, 1);
            }
            else
            {
                GameObject.Find("ItemImage").GetComponent<RawImage>().color = new Color(1, 1, 1, 0);
                GameObject.Find("ItemAmount").GetComponent<Text>().color = new Color(1, 1, 1, 0);
                _isInvetoryOpen = false;
            }
        }
        else
        {
            GameObject.Find("ItemImage").GetComponent<RawImage>().color = new Color(1, 1, 1, 0);
            GameObject.Find("ItemAmount").GetComponent<Text>().color = new Color(1, 1, 1, 0);
        }
    }
}
