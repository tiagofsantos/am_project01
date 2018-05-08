using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerAction {

    public ActionType type;
    public float timestamp;
    public bool executed;

    public PlayerAction(ActionType type, float timestamp) {
        this.type = type;
        this.timestamp = timestamp;
        executed = false;
    }

}
