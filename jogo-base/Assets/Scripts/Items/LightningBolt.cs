using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBolt : Item {
    
    public LightningBolt()
    {
        id = spriteId = 1;
        effectTimer = 0;
        effectDuration = 3;
        name = "Lightning Bolt";
    }

    public override void onConsume(Player player) {
        player.character.setModifier(Skill.SPEED, 5);
    }

    public override void onExpiration(Player player)
    {
        player.character.setModifier(Skill.SPEED, 0);
    }

}
