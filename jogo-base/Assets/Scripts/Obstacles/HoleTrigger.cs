using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTrigger : MonoBehaviour
{
    private string playerTag;

    void Start()
    {
        playerTag = "Player";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            StartCoroutine(player.movement.fade());
        }
    }
}