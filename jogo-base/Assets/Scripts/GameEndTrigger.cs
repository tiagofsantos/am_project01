using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndTrigger : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AudioManager.instance.playSound("game_end.wav");

            Player player = collision.gameObject.GetComponent<Player>();

            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            player.GetComponent<Rigidbody2D>().angularVelocity = 0f;


            //end screen e isso
        }
    }

}
