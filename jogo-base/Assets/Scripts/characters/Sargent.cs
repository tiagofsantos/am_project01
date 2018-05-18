using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sargent : Character {

    public Sargent()
    {
        setName("Sargent");
        setLevel(Skill.SPEED, 3);
        setLevel(Skill.STRENGTH, 3);
        setLevel(Skill.ENDURANCE, 9);
    }
    
}
