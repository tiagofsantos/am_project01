using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Player localPlayer;

    /* Velocidade máxima */
    private const int MAX_SPEED = 10;

    /* Velocidade máxima */
    private const float SPRINT_RATE = 1.8F;

    /* Velocidade de desaceleração */
    public const float DECELERATION_RATE = .6f;

    /* Velocidade do player */
    public float movementSpeed = 4;

    /* Potência do salto */
    public float jumpPower = 400f;

    /* Determina se o player está no chão ou no ar (grounded || airbourne) */
    public bool grounded;

    /* Rigidbody do player */
    private Rigidbody2D body;

    /* A última direção para a qual o player se estava a mover */
    private float direction;

    /* Determina se o player está a correr */
    public bool sprinting;

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        limitVelocity();
    }

    /* Limita a velocidade horizontal do jogador */
    private void limitVelocity()
    {
        int max = maxSpeed();

        if (body.velocity.x > max)
        {
            body.velocity = new Vector2(max, body.velocity.y);
        }
        else if (body.velocity.x < -max)
        {
            body.velocity = new Vector2(-max, body.velocity.y);
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
        body.velocity = new Vector2(speed() * offset, body.velocity.y);
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
        if (body.velocity.x != 0)
        {
            if (body.velocity.x < 0.01f && body.velocity.x > -0.01f)
                stop();
            else
                body.velocity = new Vector2(body.velocity.x * DECELERATION_RATE, body.velocity.y);
        }
    }

    private int maxSpeed() {
        return (int)(MAX_SPEED * sprintModifier() * speedLevelModifier());
    }

    private int speed() {
        return (int) (movementSpeed * sprintModifier() * speedLevelModifier());
    }

    private float sprintModifier() {
        return sprinting ? SPRINT_RATE : 1;
    }

    private float speedLevelModifier() {
        return 1 + ((float) localPlayer.character.getLevel(Skill.SPEED) / 50);
    }

    /* Põe a velocidade horizontal a 0 */
    public void stop()
    {
        body.velocity = new Vector2(0, body.velocity.y);
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
            body.AddForce(Vector2.up * jumpPower);
    }
}
