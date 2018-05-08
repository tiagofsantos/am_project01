using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    /* Velocidade máxima */
    private const int MAX_SPEED = 10;

    /* Velocidade de desaceleração */
    public const float DECELERATION_RATE = .9f;

    /* Velocidade do player */
    public float movementSpeed = 3;

    /* Potência do salto */
    public float jumpPower = 250f;

    /* Determina se o player está no chão ou no ar (grounded || airbourne) */
    public bool grounded;

    /* Rigidbody do player */
    private Rigidbody2D player;

    /* A última direção para a qual o player se estava a mover */
    private float direction;

    void Start()
    {
        player = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        limitVelocity();
    }

    /* Limita a velocidade horizontal do jogador */
    private void limitVelocity()
    {
        if (player.velocity.x > MAX_SPEED)
        {
            player.velocity = new Vector2(MAX_SPEED, player.velocity.y);
        }
        else if (player.velocity.x < -MAX_SPEED)
        {
            player.velocity = new Vector2(-MAX_SPEED, player.velocity.y);
        }
    }

    /* Move o jogador numa direção dita pelo offset (esquerda = -1 & direita = 1) e muda-lhe a direção.*/
    private void moveHorizontal(float offset)
    {
        /* Se o jogador mudar de direção, a velocidade volta a 0 */
        if (offset != direction)
        {
            stop();
            direction = offset;
        }

        player.AddForce((Vector2.right * movementSpeed) * offset);
        transform.localScale = new Vector3(offset, 1, 1);
    }

    /* 
     * Desacelera horizontalmente, com um rate definidoem constante.
     * Se o rate estiver muito próximo de 0, pô-mo-lo a 0 para evitar computação a mais,
     * senão, multiplicamos a acelaração horizontal pelo rate < 1, aproximando-o de 0 a
     * cada update.
     */
    public void decelerate()
    {
        if (player.velocity.x != 0)
        {
            if (player.velocity.x < 0.01f && player.velocity.x > -0.01f)
                stop();
            else
                player.velocity = new Vector2(player.velocity.x * DECELERATION_RATE, player.velocity.y);
        }
    }

    /* Põe a velocidade horizontal a 0 */
    public void stop()
    {
        player.velocity = new Vector2(0, player.velocity.y);
    }

    public void moveLeft()
    {
        moveHorizontal(-1);
    }

    public void moveRight()
    {
        moveHorizontal(1);
    }

    public void jump()
    {
        if (grounded)
            player.AddForce(Vector2.up * jumpPower);
    }



}
