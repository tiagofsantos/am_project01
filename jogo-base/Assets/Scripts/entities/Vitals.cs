using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vitals : MonoBehaviour {

    /* O limite máximo de stamina (era melhor mante-lo entre os limites de percentagem 0-100) */
    private const int MAX_STAMINA = 100;

    /* O multiplier a adicionar por cada game tick, quanto maior, mais rápido a stamina regenera */
    private const float staminaMultiplier = 0.8f;

    private float stamina;

    public Vitals()
    {
        this.stamina = 0;
    }

    /* Restaura a stamina todos os game ticks (sobe a stamina por uma pequena quantidade) */
    public void restoreStamina(int resistance)
    {
        float amount = resistance * staminaMultiplier * Time.deltaTime;

        /* ignorar quantidades negativas ou zero */
        if (amount <= 0)
            return;

        /* limitar a stamina a 100 */
        if (amount + stamina > MAX_STAMINA)
            stamina = MAX_STAMINA;
        else
            stamina += amount;
    }
}
