using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beef : Item
{
    public Beef()
    {
        id = 1;
        effectTimer = 0;
        effectDuration = 3;
        name = "Beef";
    }

    public override void onConsume(Player player)
    {
        player.character.setModifier(Skill.STRENGTH, 5);
    }

    public override void onExpire(Player player)
    {
        player.character.setModifier(Skill.STRENGTH, 0);
    }
}
