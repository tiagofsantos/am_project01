using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerActions
{
    public List<PlayerAction> actionList;

    public PlayerActions()
    {
        this.actionList = new List<PlayerAction>();
    }
}



