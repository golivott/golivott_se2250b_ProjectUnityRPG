using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 10;
    public Vector2 moveDir;
    public Animator animator;    

    
    void FixedUpdate()
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
        animator.SetFloat("Vertical",moveDir.y);
        animator.SetFloat("Magnitude",moveDir.magnitude);



        // Moving Character
        gameObject.GetComponent<Rigidbody>().velocity = moveDir * moveSpeed * Time.fixedDeltaTime;
    }
}
