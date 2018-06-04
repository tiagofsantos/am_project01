using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement movementScript;

    void Update()
    {
        if (movementScript != null) {

            if (Input.GetButtonDown("Jump"))
            {
                movementScript.jump();
            }

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                movementScript.decelerate();
            }else if (Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                movementScript.moveLeft();
            }else if (Input.GetAxisRaw("Horizontal") > 0.1f)
            {
                movementScript.moveRight();
            }
            
        }
        
    }

    
}
