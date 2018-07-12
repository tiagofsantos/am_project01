using System;
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

    /* Adiciona uma nova ação, no tempo actual. */
    public void addAction(ActionType type)
    {
        actions.Add(new PlayerAction(type, Time.time, ticks));
    }

}
