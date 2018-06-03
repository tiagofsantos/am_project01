using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    /* O player ao qual este script está associado */
    private Player localPlayer;

    public List<PlayerAction> actions;

    private PlayerAction lastAction;
    private int executions;
    private int count;


    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
    }

    void Update()
    {
        float currentTime = Time.time;
        count++;

        foreach (PlayerAction action in actions)
        {
            if (currentTime > action.timestamp && !action.executed)
            {
                lastAction = action;
                action.executed = true;
                executeAction(action);
                executions = 1;
            }
            else
            {
                if (lastAction != null)
                {
                    PlayerAction next = null;
                    int currentIndex = actions.IndexOf(lastAction);

                    if (currentIndex + 1 < actions.Count)
                        next = actions[currentIndex + 1];

                    if (next != null && executions < next.previousExecutions)
                    {
                        executeAction(lastAction);
                        executions++;
                    }

                }
            }
        }
    }

    private void executeAction(PlayerAction action)
    {
        if (action.type == ActionType.JUMP)
        {
            localPlayer.movement.jump();
        }

        switch (action.type)
        {
            case ActionType.STOP:
                localPlayer.movement.stop();
                break;
            case ActionType.MOVE_LEFT:
                localPlayer.movement.moveLeft();
                break;
            case ActionType.MOVE_RIGHT:
                localPlayer.movement.moveRight();
                break;
        }
    }

}
