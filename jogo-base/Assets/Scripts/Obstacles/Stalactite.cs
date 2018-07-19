using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    /* Tempo que demora até a estalactite começar a cair, em segundos */
    public float timeBeforeFall = 3f;

    /* Velocidade de queda da estalactite */
    private const float FALL_SPEED = 15.0f;

    /* Posição Y onde o objecto será destruído, para não cair infinitamente. */
    public float DESTRUCTION_POSITION = -10;

    /* Bool que afirma se o objecto está a cair. 
     * A estalactite apenas cai quando activated é true.
     * A variável fica true quando passa o tempo definido em TIME_BEFORE_FALL
     */
    private bool activated;

    private bool hitPlayer;

    void Start()
    {
        // Inicia o temporizador, no final do qual o objecto começa a cair.
        StartCoroutine(timer());
        hitPlayer = false;
    }

    void Update()
    {
        /* Se activated for true, o temporizador acabou, e o objecto começa a cair. */
        if (activated)
        {
            transform.Translate(Vector3.down * FALL_SPEED * Time.deltaTime, Space.World);

            /* Destroí o objecto se ele já estiver abaixo da DESTRUCTION_POSITION. */
            if (this.transform.position.y <= DESTRUCTION_POSITION)
            {
                if (!hitPlayer)
                {
                    Destroy(gameObject);
                }
                else
                {
                    StartCoroutine(destroy());
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            hitPlayer = true;

            Player player = other.gameObject.GetComponent<Player>();
            
            if (player.movement.respawning || player.movement.phasing)
            {
                return;
            }

            AudioManager.instance.playSound("stalactite_hit.wav");
            StartCoroutine(player.movement.fadeout());
        }
    }

    /* Temporizador. 
     * Inicia activated a false, e depois de esperar pelos segundos definidos em TIME_BEFORE_FALL,
     * aletera o valor da variável para true.
     */
    private IEnumerator timer()
    {
        activated = false;
        yield return new WaitForSeconds(timeBeforeFall);
        activated = true;
    }

    private IEnumerator destroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
