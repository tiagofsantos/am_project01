using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ActionTracker : MonoBehaviour {

    private static PlayerActions actions;

	void Start () {
        actions = new PlayerActions();
	}

    /* Adiciona uma nova ação, no tempo actual. */
    public static void addAction(ActionType type) {
        actions.actionList.Add(new PlayerAction(type, Time.time));
    }
    
}
