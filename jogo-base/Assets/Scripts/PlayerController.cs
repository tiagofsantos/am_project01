using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Vector3 respawnPoint;
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

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position;
        }
    }

    public void Respawn()
    {
        player.transform.position = respawnPoint;
    }
}
