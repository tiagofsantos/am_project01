using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sargent : Character {

    public Sargent()
    {
        setName("Sargent");
        setLevel(Skill.Speed, 3);
        setLevel(Skill.Strength, 3);
        setLevel(Skill.Resistance, 9);
    }
    
}
