using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction {

    public ActionType type;
    public float timestamp;
    public int previousExecutions;
    public bool executed;

    public PlayerAction(ActionType type, float timestamp, int previousExecutions) {
        this.type = type;
        this.timestamp = timestamp;
        this.previousExecutions = previousExecutions;
        executed = false;
    }

}
