using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Player : MonoBehaviour
{
    public User user;

    public Character character;
    private Vitals vitals;
    private Inventory inventory;

    private bool sprinting;

    public Player(User user, Character character) {
        init(user, character);
    }
    
    public void init(User user, Character character)
    {
        this.character = character;
        this.user = user;

        vitals = new Vitals(character);
        sprinting = false;
        inventory = new Inventory();
    }

    void Update()
    {
        if (sprinting)
            Debug.Log("Alterar velocidade aqui");
        else
            vitals.restoreStamina();

        vitals.Update();
    }

    public void stun()
    {
        vitals.stun();
    }

}
