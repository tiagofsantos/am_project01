using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour
{
    /* O player ao qual este script está associado */
    private Player localPlayer;

    public List<PlayerAction> actions;
    public List<PlayerAction> executing;

    private int ticks;


    void Start()
    {
        localPlayer = gameObject.GetComponent<Player>();
        executing = new List<PlayerAction>();
    }

    void Update()
    {
        ticks++;

        List<PlayerAction> toRemove = new List<PlayerAction>();

        foreach (PlayerAction action in actions)
        {
            if (action.startTick == ticks)
            {
                executing.Add(action);
                toRemove.Add(action);
            }
        }

        foreach (PlayerAction remove in toRemove)
        {
            actions.Remove(remove);
        }

        toRemove.Clear();

        foreach (PlayerAction action in executing)
        {
            if (action.endTick == ticks)
            {
                endAction(action);
                toRemove.Add(action);
            }
            else
            {
                executeAction(action);
            }
        }

        foreach (PlayerAction remove in toRemove)
        {
            executing.Remove(remove);
        }
    }

    private void endAction(PlayerAction action)
    {
        if (action.type == ActionType.SPRINT)
        {
            localPlayer.movement.sprinting = false;
        }

        if (action.type == ActionType.MOVE_LEFT || action.type == ActionType.MOVE_RIGHT)
        {
            localPlayer.movement.stop();
        }
    }

    private void executeAction(PlayerAction action)
    {
        if (action.type == ActionType.JUMP)
        {
            localPlayer.movement.jump();
        }

        if (action.type == ActionType.SPRINT)
        {
            localPlayer.movement.sprinting = true;
        }

        if (action.type == ActionType.CONSUME_1)
        {
            localPlayer.inventory.consume(0);
        }

        if (action.type == ActionType.CONSUME_2)
        {
            localPlayer.inventory.consume(1);
        }

        if (action.type == ActionType.CONSUME_3)
        {
            localPlayer.inventory.consume(2);
        }

        if (action.type == ActionType.CONSUME_4)
        {
            localPlayer.inventory.consume(3);
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
