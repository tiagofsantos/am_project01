using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement playerMovement;
    
	void Start () {
        playerMovement = gameObject.GetComponentInParent<PlayerMovement>();
	}
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            return;
        }

        playerMovement.grounded = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            return;
        }
        
        playerMovement.grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            return;
        }

        playerMovement.grounded = false;
    }

}
