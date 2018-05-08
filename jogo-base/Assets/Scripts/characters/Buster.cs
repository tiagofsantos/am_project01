using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buster : Character {

    public Buster()
    {
        setName("Buster");
        setLevel(Skill.Speed, 3);
        setLevel(Skill.Strength, 9);
        setLevel(Skill.Resistance, 3);
    }
    
}
