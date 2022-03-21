using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10;
    public Vector2 moveDir;
    void FixedUpdate()
    {

        // Calculating move direction
        moveDir = Vector2.zero;
        
        if (Input.GetKey(KeyCode.W))
            moveDir.y = 1;
        if (Input.GetKey(KeyCode.S))
            moveDir.y = -1;
        if (Input.GetKey(KeyCode.A))
            moveDir.x = 1;
        if (Input.GetKey(KeyCode.D))
            moveDir.x = -1;   

        // Moving Character
        gameObject.GetComponent<Rigidbody2D>().velocity = moveDir * speed * Time.fixedDeltaTime;
    }
}
