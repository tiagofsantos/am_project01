using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character
{
    public string name;

    /* Níveis base do character */
    private Dictionary<Skill, int> skillSet;

    /* Modificadores do character (Boosts temporários) */
    private Dictionary<Skill, int> modifiers;

    /* Nível máximo total (soma dos 3 atributos) */
    private const int MAX_TOTAL_LEVEL = 15;

    /* Nível máximo de cada atributo */
    public const int MAX_SKILL_LEVEL = 10;

    public Character()
    {
        skillSet = new Dictionary<Skill, int>();
        skillSet.Add(Skill.SPEED, 0);
        skillSet.Add(Skill.STRENGTH, 0);
        skillSet.Add(Skill.ENDURANCE, 0);

        modifiers = new Dictionary<Skill, int>();
        modifiers.Add(Skill.SPEED, 0);
        modifiers.Add(Skill.STRENGTH, 0);
        modifiers.Add(Skill.ENDURANCE, 0);
    }

    /* Nível (falso/temporário) de um certo skill, incluindo modificadores (boosts). */ 
    public int getLevel(Skill skill)
    {
        return skillSet[skill] + modifiers[skill];
    }

    /* Nível real de um certo skill, excluindo modificadores (boosts). */
    public int getRealLevel(Skill skill)
    {
        return skillSet[skill];
    }

    /* Remover boost de um certo skill */
    public void resetModifier(Skill skill) {
        modifiers[skill] = 0;
    }

    /* Adicionar boost a um certo skill */
    public void setModifier(Skill skill, int value) {
        modifiers[skill] = value;
    }

    /* Altera o nivel de um certo skill para o valor dado, verificando niveis máximos.*/
    public void setLevel(Skill skill, int value)
    {
        /* Nível acima do valor máximo (10)*/
        if (value > MAX_SKILL_LEVEL)
        {
            Debug.LogError("Nível (" + skill + ":" + value + ") demasiado alto. MAX = " + MAX_SKILL_LEVEL);
            return;
        }

        /* Nível total acima do valor máximo (15)*/
        if (totalSkillLevel() + value > MAX_TOTAL_LEVEL)
        {
            Debug.LogError("Nível total demasiado alto, (" + (totalSkillLevel() + value) + "). MAX = " + MAX_TOTAL_LEVEL);
            return;
        }

        skillSet[skill] = value;
    }

    /* Retorna a soma de todos os níveis (nível total) */
    private int totalSkillLevel()
    {
        int count = 0;

        foreach (KeyValuePair<Skill, int> entry in skillSet)
        {
            count += entry.Value;
        }

        return count;
    }
}
