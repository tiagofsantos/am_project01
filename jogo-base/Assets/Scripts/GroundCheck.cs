using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {

    private PlayerMovement player;
    
	void Start () {
        player = gameObject.GetComponentInParent<PlayerMovement>();
	}
	
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision)
    {
        player.grounded = true;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        player.grounded = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        player.grounded = false;
    }

    



}
