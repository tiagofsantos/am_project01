using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : Item
{
    public Star()
    {
        id = 2;
        effectTimer = 0;
        effectDuration = 3;
        name = "Star";
    }

    public override void onConsume(Player player)
    {
        player.movement.phasing = true;
    }

    public override void onExpire(Player player)
    {
        player.movement.phasing = false;
    }
}

