using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour {

    private PlayerController player;
    private string playerTag;

	// Use this for initialization
	void Start () {
        playerTag = "Player";
        player = GameObject.FindGameObjectWithTag(playerTag).GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            player.Respawn();
        }
    }
}
