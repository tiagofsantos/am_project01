using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    MOVE_RIGHT,
    MOVE_LEFT,
    JUMP,
    STOP  
}

public static class ActionTypeHandler
{
    public static ActionType getActionType(string actionType)
    {
        switch (actionType){
            case "MOVE_RIGHT": return ActionType.MOVE_RIGHT;
            case "MOVE_LEFT": return ActionType.MOVE_LEFT;
            case "JUMP": return ActionType.JUMP;
            case "STOP": return ActionType.STOP;
            default:  return ActionType.MOVE_RIGHT; ;
        }
    }
}
