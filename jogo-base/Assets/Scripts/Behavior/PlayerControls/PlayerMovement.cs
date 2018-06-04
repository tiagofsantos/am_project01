using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* O player ao qual este script está associado */
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

    /* Posição do último checkpoint onde o player passou.*/
    private Vector3 respawnPoint;

    /* Verifica se o jogador está a "morrer" */
    public bool respawning;

    /* Determina se o player está a correr */
    public bool sprinting;

    /* Determina se o player está imune a projéteis (phasing = atravessa os projeteis) */
    public bool phasing;

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
        body = gameObject.GetComponent<Rigidbody2D>();

        respawnPoint = body.transform.position;
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
        /* Apenas deixa o jogador se movimentar se possível */
        if (respawning || localPlayer.vitals.isStunned())
            return;

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

    private int maxSpeed()
    {
        return (int)(MAX_SPEED * sprintModifier() * speedLevelModifier());
    }

    private int speed()
    {
        return (int)(movementSpeed * sprintModifier() * speedLevelModifier());
    }

    private float sprintModifier()
    {
        return sprinting ? SPRINT_RATE : 1;
    }

    private float speedLevelModifier()
    {
        return 1 + ((float)localPlayer.character.getLevel(Skill.SPEED) / 50);
    }

    private float jumpPowerModifier()
    {
        return 1 + ((float)localPlayer.character.getLevel(Skill.STRENGTH) / 25);
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
        if (respawning || !grounded || localPlayer.vitals.isStunned())
            return;

        body.AddForce(Vector2.up * jumpPower * jumpPowerModifier());
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        /* Atualiza a posição do último Checkpoint ao passar por um novo */
        if (other.CompareTag("Checkpoint"))
        {
            respawnPoint = other.transform.position;
        }
    }

    /* Move o player para a posição do último checkpoint */
    public void respawn()
    {
        body.transform.position = respawnPoint;
    }

    /*Faz o jogador desaparecer e reaparecer no último checkpoint.*/
    public IEnumerator fade()
    {
        respawning = true; // O jogador deixa de poder controlar a personagem
        SpriteRenderer renderer = this.GetComponent<SpriteRenderer>();
        Color color = renderer.material.color;
        float speed = 0.1f; // Percisa de 10 iterações
        float wait = 0.1f; // Tempo que espera depois de cada iteração. Quando a resistência muda este valor muda, a speed fica sempre igual. formula = (0,1) - (resistencia / 100)

        if (wait == 0)
        {
            wait = 0.009f;
        }

        //fade out
        while (color.a > 0)
        {
            color = renderer.material.color;

            color.a -= speed;

            renderer.material.color = color;

            yield return new WaitForSeconds(wait); // Duração = wait / speed
        }
        body.velocity = Vector3.zero;
        body.angularVelocity = 0;

        //volta ao checkpoint
        respawn();

        respawning = false; // O jogador volta a ganhar controlo da personagem quando esta volta ao checkpoint

        //fade in
        while (color.a < 1)
        {
            color = renderer.material.color;

            color.a += speed;

            renderer.material.color = color;

            yield return new WaitForSeconds(wait);
        }
    }

}
