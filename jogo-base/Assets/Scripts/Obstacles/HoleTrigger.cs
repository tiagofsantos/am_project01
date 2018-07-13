using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    /* Tag definida para o jogador */
    private const string PLAYER_TAG = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            StartCoroutine(player.movement.fadeout());
        }
    }
}