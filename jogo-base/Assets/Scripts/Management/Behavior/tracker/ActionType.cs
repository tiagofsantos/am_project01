using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MOVE_RIGHT,
    MOVE_LEFT,
    JUMP,
    STOP,
    CONSUME_1,
    CONSUME_2,
    CONSUME_3,
    CONSUME_4,
    SPRINT
}

public static class ActionTypeHandler
{
    public static ActionType getActionType(string actionType)
    {
        switch (actionType)
        {
            case "CONSUME_1":
                return ActionType.CONSUME_1;
            case "CONSUME_2":
                return ActionType.CONSUME_2;
            case "CONSUME_3":
                return ActionType.CONSUME_3;
            case "CONSUME_4":
                return ActionType.CONSUME_4;
            case "SPRINT":
                return ActionType.SPRINT;
            case "MOVE_RIGHT":
                return ActionType.MOVE_RIGHT;
            case "MOVE_LEFT":
                return ActionType.MOVE_LEFT;
            case "JUMP":
                return ActionType.JUMP;
            default:
                return ActionType.STOP;
        }
    }
}
