using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Character {

    public Scout()
    {
        setName("Scout");
        setLevel(Skill.Speed, 9);
        setLevel(Skill.Strength, 3);
        setLevel(Skill.Resistance, 3);
    }
    
}
