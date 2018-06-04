using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

    /* Jogador */
    private PlayerMovement player;

    /* Tag definida para o jogador */
    private const string PLAYER_TAG = "Player";


    void Start()
    {
        player = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<PlayerMovement>();
    }

    void Update () {
		
	}

    /* Quando o player colide com o spike, inicia o método Fade em player, e Knockback, executando os dois simultaneamente. */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PLAYER_TAG))
        {
            /* Verifica se o Player está respawning ou não.
             * Sem a verificação, os efeitos do spike podem ser aplicados múltiplas vezes. */
            if (!other.GetComponent<PlayerMovement>().respawning)
            {
                StartCoroutine(player.Fade());
                Knockback(other);
            }
        }
    }

    public void Knockback(Collider2D col)
    {
        col.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        float speed = 350;

        Vector3 direction = col.transform.position - transform.position;
        
        col.GetComponent<Rigidbody2D>().AddForce(direction * speed);
    }

}
