using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public PlayerMovement movementScript;

    void Update()
    {
        if (movementScript != null) {

            if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movementScript.jump();
                ActionTracker.addAction(ActionType.JUMP);
            }

            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                movementScript.decelerate();
                ActionTracker.addAction(ActionType.DECELERATE);
            }
            else if (Input.GetAxisRaw("Horizontal") < -0.1f)
            {
                movementScript.moveLeft();
                ActionTracker.addAction(ActionType.MOVE_LEFT);
            }
            else if (Input.GetAxisRaw("Horizontal") > 0.1f)
            {
                movementScript.moveRight();
                ActionTracker.addAction(ActionType.MOVE_RIGHT);
            }
            
        }
        
    }


}
