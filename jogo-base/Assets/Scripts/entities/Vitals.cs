using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour
{
    /* O player ao qual os vitais estão associados */
    private Player localPlayer;

    /* O limite máximo de stamina (era melhor mante-lo entre os limites de percentagem 0-100) */
    private const int MAX_STAMINA = 100;

    /* O multiplier a adicionar por cada game tick, quanto maior, mais rápido a stamina regenera */
    private const float STAMINA_RESTORE_MULTIPLIER = .35f;

    /* O multiplier a reduzir por cada game tick em sprint, quanto maior, mais rápido se gasta a stamina */
    private const float STAMINA_MULTIPLIER = 4f;

    /* O nível de stamina do player (0 - MAX_STAMINA) */
    public float stamina;

    /* O contador de stun, 0 = !stunned */
    private float stunClock;

    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();

        stunClock = 0;
        stamina = 0;
    }

    void Update()
    {
        /* Se o player não está a correr, restaurar stamina */
        if (localPlayer.movement.sprinting)
            reduceStamina();
        else
            restoreStamina();

        if (stamina == 0 && localPlayer.movement.sprinting)
        {
            localPlayer.movement.sprinting = false;
        }

        /* Se está stunned, decrescer o contador */
        if (isStunned())
            stunClock -= Time.deltaTime;

        /* Se o contador passou do limite minimo, igualar a 0 */
        if (stunClock < 0)
            stunClock = 0;
    }

    /* Restaura a stamina todos os game ticks (sobe a stamina por uma pequena quantidade) */
    public void restoreStamina()
    {
        float amount = 0.065f + (localPlayer.character.getLevel(Skill.ENDURANCE) * STAMINA_RESTORE_MULTIPLIER * Time.deltaTime);

        /* ignorar quantidades negativas ou zero */
        if (amount <= 0)
            return;

        /* limitar a stamina a 100 */
        if (amount + stamina > MAX_STAMINA)
            stamina = MAX_STAMINA;
        else
            stamina += amount;
    }

    public void reduceStamina()
    {
        float amount = 0.06f + (localPlayer.character.getLevel(Skill.ENDURANCE) * Time.deltaTime * STAMINA_MULTIPLIER);

        /* ignorar quantidades negativas ou zero */
        if (amount <= 0)
            return;

        /* limitar a stamina a 100 */
        if (stamina - amount < 0)
            stamina = 0;
        else
            stamina -= amount;
    }

    /* Recomeça o contador de stun para o valor dado pela fórmula stunPenalty */
    public void stun()
    {
        stunClock = stunPenalty();
    }

    public bool isStunned()
    {
        return stunClock > 0;
    }

    /* Fórmula temporária para definir o tempo de espera 
     * do player baseado no nivel de resistencia
     * 
     * Ex: Scout = ((10-3) / 2 ) + 2) = 5 (seria 5.5 se não houvesse arredondamento de ints)
     * Ex: Sargent = ((10-9) / 2 ) + 2) = 2 (seria 2.5 se não houvesse arredondamento de ints)
     * 
     * Desta forma, o tempo de espera desce quando o nível de resistência sobe.
     * */
    public float stunPenalty()
    {
        return ((Character.MAX_SKILL_LEVEL - localPlayer.character.getLevel(Skill.ENDURANCE)) / 2) + 2;
    }
}
