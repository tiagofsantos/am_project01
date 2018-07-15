using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scout : Character
{

    public Scout()
    {
        name = "Scout";
        setLevel(Skill.SPEED, 9);
        setLevel(Skill.STRENGTH, 3);
        setLevel(Skill.ENDURANCE, 3);
    }
}
