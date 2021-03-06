﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionTracker : MonoBehaviour
{
    public List<PlayerAction> actions;
    public int ticks;

    void Start()
    {
        actions = new List<PlayerAction>();
    }

    private void Update()
    {
        ticks++;
    }

    /* Adiciona uma nova ação, no tick actual. */
    public void addAction(ActionType type)
    {
        actions.Add(new PlayerAction(type, ticks));
    }

    /* Adiciona uma nova ação, no tick actual, com uma duração especifica. */
    public void addAction(ActionType type, int duration)
    {
        actions.Add(new PlayerAction(type, ticks, ticks + duration));
    }

    public void closeAction(ActionType type)
    {
        foreach (PlayerAction action in actions)
        {
            if (action.type == type && action.endTick == -1)
            {
                action.endTick = ticks;
                return;
            }
        }
    }

}
