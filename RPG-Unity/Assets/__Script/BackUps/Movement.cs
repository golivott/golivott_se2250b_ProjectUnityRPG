using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Movement : MonoBehaviour
{
    private float _moveSpeed = 350; //default movespeed
    public Vector2 moveDir; //move direction
    public Animator animator; //animation object
    public bool disableMovement; //disables movement
    [FormerlySerializedAs("_player")] public Player player; //player object
    
    private float _activeMoveSpeed; //the movespeed that gets used
    public float dashSpeed; //speed of dash 

    public float dashLength = 0.2f, dashCooldown = 1f; //0.2s is length of dash and 1s dash cooldown

    private float _dashCounter; //doesn't allow you to dash while a dash is already in process
    private float _dashCoolCounter; //dash cooldown

    //sets movespeed to default speed and gets gameobject
    void Start(){
        _activeMoveSpeed = _moveSpeed;
        player = gameObject.GetComponent<Player>();
    }


    void FixedUpdate()
    {   
        if (!disableMovement) //checks to see if movement isn't disabled
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

            //sets the animator floats to their respect axis and magnitude to allow for movement animations for player
            animator.SetFloat("Horizontal", moveDir.x);
            animator.SetFloat("Vertical", moveDir.y);
            animator.SetFloat("Magnitude", moveDir.magnitude);

            //Code for the dash
            if (Input.GetKey(KeyCode.Space))
            {
                if (_dashCoolCounter <= 0 && _dashCounter <= 0) //checks cooldown and to see if a dash is already occuring, if not then does dash
                {
                    _activeMoveSpeed = dashSpeed;
                    _dashCounter = dashLength;
                    player.SetCannotTakeDamage(); //gives invulnerability
                }
            }

            if (_dashCounter > 0) //doesn't equal zero until a dash is done
            {
                _dashCounter -= Time.fixedDeltaTime;

                if (_dashCounter <= 0) //when dashCounter is zero, dash is over 
                {
                    _activeMoveSpeed = _moveSpeed;
                    _dashCoolCounter = dashCooldown;
                    player.SetCanTakeDamage(); //takes away invulnerability
                }
            }

            if (_dashCoolCounter > 0) //countdown for dash
            {
                _dashCoolCounter -= Time.fixedDeltaTime;
            }

            // Moving Character
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDir.normalized * _activeMoveSpeed * Time.fixedDeltaTime; //allows the player to move
        }
    }
}
