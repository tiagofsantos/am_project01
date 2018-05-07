using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionReplay : MonoBehaviour {

    private PlayerActions actions;

	void Start () {
        this.actions = ActionTracker.Load();
	}
	
	void Update () {

        float currentTime = Time.time;

        foreach (PlayerAction action in actions.actionList) {
            if (currentTime > action.timestamp && !action.executed) {
                //executar ação aqui
            }
        }

	}
}
