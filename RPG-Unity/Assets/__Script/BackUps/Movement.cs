using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 350; //default movespeed
    public Vector2 moveDir; //move direction
    public Animator animator; //animation object
    public bool disableMovement; //disables movement
    public Player _player; //player object
    
    private float activeMoveSpeed; //the movespeed that gets used
    public float dashSpeed; //speed of dash 

    public float dashLength = 0.2f, dashCooldown = 1f; //0.2s is length of dash and 1s dash cooldown

    private float dashCounter; //doesn't allow you to dash while a dash is already in process
    private float dashCoolCounter; //dash cooldown

    //sets movespeed to default speed and gets gameobject
    void Start(){
        activeMoveSpeed = moveSpeed;
        _player = gameObject.GetComponent<Player>();
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
                if (dashCoolCounter <= 0 && dashCounter <= 0) //checks cooldown and to see if a dash is already occuring, if not then does dash
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    _player.SetCannotTakeDamage(); //gives invulnerability
                }
            }

            if (dashCounter > 0) //doesn't equal zero until a dash is done
            {
                dashCounter -= Time.fixedDeltaTime;

                if (dashCounter <= 0) //when dashCounter is zero, dash is over 
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                    _player.SetCanTakeDamage(); //takes away invulnerability
                }
            }

            if (dashCoolCounter > 0) //countdown for dash
            {
                dashCoolCounter -= Time.fixedDeltaTime;
            }

            // Moving Character
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDir.normalized * activeMoveSpeed * Time.fixedDeltaTime; //allows the player to move
        }
    }
}
