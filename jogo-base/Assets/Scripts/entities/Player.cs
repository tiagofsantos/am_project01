using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Character character;
    private Vitals vitals;
    private Inventory inventory;

    private bool sprinting;

    public Player(Character character)
    {
        this.character = character;
        sprinting = false;
        inventory = new Inventory();
        vitals = new Vitals(character.getLevel(Skill.Resistance));
        Debug.Log(vitals == null);
    }
    
    void Update()
    {
       // Debug.Log(vitals == null);
        /*
        if (sprinting)
            Debug.Log("Alterar velocidade aqui");
        else
            vitals.restoreStamina();

        vitals.Update();*/
    }

    public void stun()
    {
        vitals.stun();
    }

}
