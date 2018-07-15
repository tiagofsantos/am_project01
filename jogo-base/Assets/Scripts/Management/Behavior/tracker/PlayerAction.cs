using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction {

    public ActionType type;
    public int startTick;
    public int endTick;

    public PlayerAction(ActionType type, int startTick, int endTick) {
        this.type = type;
        this.startTick = startTick;
        this.endTick = endTick;
    }

    public PlayerAction(ActionType type, int startTick)
    {
        this.type = type;
        this.startTick = startTick;
        this.endTick = -1;
    }


}
