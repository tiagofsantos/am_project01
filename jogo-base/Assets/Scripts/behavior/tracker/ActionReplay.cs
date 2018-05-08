using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{

    public PlayerMovement movementScript;
    private PlayerActions actions;

    void Start()
    {
       // this.actions = ActionTracker.Load();
    }

    void Update()
    {

        /*
        if (movementScript == null)
            return;

        float currentTime = Time.time;

        foreach (PlayerAction action in actions.actionList)
        {
            if (currentTime > action.timestamp && !action.executed)
            {

                switch (action.type)
                {
                    case ActionType.DECELERATE:
                        movementScript.decelerate();
                        break;
                    case ActionType.MOVE_LEFT:
                        movementScript.moveLeft();
                        break;
                    case ActionType.MOVE_RIGHT:
                        movementScript.moveRight();
                        break;
                    case ActionType.JUMP:
                        movementScript.jump();
                        break;
                }

                action.executed = true;

            }
        }*/
    }

}
