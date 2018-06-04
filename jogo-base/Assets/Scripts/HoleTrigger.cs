using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour {

    private PlayerMovement player;
    private string playerTag;
    
	void Start () {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerMovement>();
	}
	
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            StartCoroutine(player.Fade());
        }
    }
}
