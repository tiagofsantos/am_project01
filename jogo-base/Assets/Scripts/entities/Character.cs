using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character {

    private string name;
    private Dictionary<Skill, int> skillSet;

    private const int MAX_TOTAL_LEVEL = 15;
    private const int MAX_SKILL_LEVEL = 10;

    public Character()
    {
        this.skillSet = new Dictionary<Skill, int>();
        this.skillSet.Add(Skill.Speed, 0);
        this.skillSet.Add(Skill.Strength, 0);
        this.skillSet.Add(Skill.Resistance, 0);
    }

    public void setName(string name) {
        this.name = name;
    }

    public int getLevel(Skill skill)
    {
        return this.skillSet[skill];
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
        if (totalSkillLevel() + value > MAX_TOTAL_LEVEL) {
            Debug.LogError("Nível total demasiado alto, ("+ (totalSkillLevel() + value) +"). MAX = " + MAX_TOTAL_LEVEL);
            return;
        }

        this.skillSet[skill] = value;
    }

    /* Retorna a soma de todos os níveis (nível total) */ 
    private int totalSkillLevel() {

        int count = 0;
        foreach (KeyValuePair<Skill, int> entry in skillSet)
        {
            count += entry.Value;
        }
        return count;
    }
}
