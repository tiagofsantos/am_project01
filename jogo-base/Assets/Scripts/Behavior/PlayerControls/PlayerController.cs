using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement movementScript;
    private ActionTracker trackerScript;

    private void Start()
    {
        movementScript = gameObject.GetComponentInParent<PlayerMovement>();
        trackerScript = gameObject.GetComponentInParent<ActionTracker>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            movementScript.jump();
            trackerScript.addAction(ActionType.JUMP);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            movementScript.sprinting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            movementScript.sprinting = false;
        }


        if (Input.GetAxisRaw("Horizontal") == 0)
        {
            movementScript.stop();
            trackerScript.addAction(ActionType.STOP);
        }
        else if (Input.GetAxisRaw("Horizontal") < -0.1f)
        {
            movementScript.moveLeft();
            trackerScript.addAction(ActionType.MOVE_LEFT);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0.1f)
        {
            movementScript.moveRight();
            trackerScript.addAction(ActionType.MOVE_RIGHT);
        }

    }

}
