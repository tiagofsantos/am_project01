using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buster : Character
{
    public Buster()
    {
        name = "Buster";
        setLevel(Skill.SPEED, 3);
        setLevel(Skill.STRENGTH, 9);
        setLevel(Skill.ENDURANCE, 3);
    }
}
