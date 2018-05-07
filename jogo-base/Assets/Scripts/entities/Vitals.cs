using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals
{

    /* O limite máximo de stamina (era melhor mante-lo entre os limites de percentagem 0-100) */
    private const int MAX_STAMINA = 100;

    /* O multiplier a adicionar por cada game tick, quanto maior, mais rápido a stamina regenera */
    private const float STAMINA_MULTIPLIER = 0.8f;

    /* O nível de resistencia do player, definido na inicialização da classe */
    private int resistanceLevel;

    /* O nível de stamina do player (0 - MAX_STAMINA) */
    private float stamina;

    /* O contador de stun, 0 = !stunned */
    private float stunClock;

    public Vitals(int resistanceLevel)
    {
        stunClock = 0;
        stamina = 0;
        this.resistanceLevel = resistanceLevel;
    }

    /* Restaura a stamina todos os game ticks (sobe a stamina por uma pequena quantidade) */
    public void restoreStamina()
    {
        float amount = resistanceLevel * STAMINA_MULTIPLIER * Time.deltaTime;

        /* ignorar quantidades negativas ou zero */
        if (amount <= 0)
            return;

        /* limitar a stamina a 100 */
        if (amount + stamina > MAX_STAMINA)
            stamina = MAX_STAMINA;
        else
            stamina += amount;
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
    private float stunPenalty()
    {
        return ((Character.MAX_SKILL_LEVEL - resistanceLevel) / 2) + 2;
    }

    public void Update()
    {
        if (stunClock > 0)
        {
            stunClock -= Time.deltaTime;
        }

        if (stunClock < 0)
            stunClock = 0;

        Debug.Log(stunClock);
    }

}
