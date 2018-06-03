using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTracker : MonoBehaviour
{
    public List<PlayerAction> actions;
    private int executions;

    void Start()
    {
        actions = new List<PlayerAction>();
    }

    /* Adiciona uma nova ação, no tempo actual. */
    public void addAction(ActionType type)
    {
        if (actions.Count == 0 || actions[actions.Count - 1].type != type)
        {
            actions.Add(new PlayerAction(type, Time.time, executions));
            executions = 0;
        }
        else
            executions++;
    }

}
