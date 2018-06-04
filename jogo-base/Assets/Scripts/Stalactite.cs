using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour {

    /* Tempo que demora até a estalactite começar a cair, em segundos */
    private const float TIME_BEFORE_FALL = 3f;

    /* Velocidade de queda da estalactite */
    private const float FALL_SPEED = 8.0f;

    /* Posição Y onde o objecto será destruído, para não cair infinitamente. */
    private const float DESTRUCTION_POSITION = -10;

    /* Bool que afirma se o objecto está a cair. 
     * A estalactite apenas cai quando activated é true.
     * A variável fica true quando passa o tempo definido em TIME_BEFORE_FALL*/
    private bool activated;


    void Start () {
        // Inicia o temporizador, no final do qual o objecto começa a cair.
        StartCoroutine(Timer());
	}
	

	void Update () {
        // Se activated for true, o temporizador acabou, e o objecto começa a cair.
        if (activated)
        {
            transform.Translate(Vector3.down * FALL_SPEED * Time.deltaTime, Space.World);

            // Destroí o objecto se ele já estiver abaixo da DESTRUCTION_POSITION.
            if (this.transform.position.y <= DESTRUCTION_POSITION)
                Destroy(this);
        }
	}

    /* Temporizador. 
     * Inicia activated a false, e depois de esperar pelos segundos definidos em TIME_BEFORE_FALL,
     * aletera o valor da variável para true.*/
    public IEnumerator Timer()
    {
        activated = false;
        yield return new WaitForSeconds(TIME_BEFORE_FALL);
        activated = true;
    }
}

