using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private float moveSpeed = 350;
    public Vector2 moveDir;
    public Animator animator;
    public bool disableMovement;
    public Player _player;
    
    private float activeMoveSpeed;
    public float dashSpeed;

    public float dashLength = 0.2f, dashCooldown = 1f;

    private float dashCounter;
    private float dashCoolCounter;

    void Start(){
        activeMoveSpeed = moveSpeed;
         _player = gameObject.GetComponent<Player>();
    }

    void FixedUpdate()
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
                if (dashCoolCounter <= 0 && dashCounter <= 0)
                {
                    activeMoveSpeed = dashSpeed;
                    dashCounter = dashLength;
                    _player.SetCannotTakeDamage();
                }
            }

            if (dashCounter > 0)
            {
                dashCounter -= Time.fixedDeltaTime;

                if (dashCounter <= 0)
                {
                    activeMoveSpeed = moveSpeed;
                    dashCoolCounter = dashCooldown;
                    _player.SetCanTakeDamage();
                }
            }

            if (dashCoolCounter > 0)
            {
                dashCoolCounter -= Time.fixedDeltaTime;
            }

            // Moving Character
            gameObject.GetComponent<Rigidbody2D>().velocity = moveDir * activeMoveSpeed * Time.fixedDeltaTime;
        }
    }
}
