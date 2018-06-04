using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public User user;

    public Vitals vitals;
    public Character character;
    public Inventory inventory;

    public PlayerController controller;
    public PlayerMovement movement;

    public ActionReplay replay;
    public ActionTracker tracker;

    void Start()
    {
        vitals = gameObject.GetComponent<Vitals>();
        inventory = gameObject.GetComponent<Inventory>();

        //General script, all players have this.
        movement = gameObject.GetComponentInParent<PlayerMovement>();

        //Player script, only the local player has this.
        controller = gameObject.GetComponentInParent<PlayerController>();
        tracker = gameObject.GetComponentInParent<ActionTracker>();

        //Shadow script, only the opponent player (shadow) has this.
        replay = gameObject.GetComponentInParent<ActionReplay>();
    }
}
