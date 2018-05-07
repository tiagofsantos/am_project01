using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Character character;
    private Vitals vitals;
    private Inventory inventory;

    private bool sprinting;

    public Player()
    {
        sprinting = false;
        inventory = new Inventory();
    }

    void Start()
    {
        character = new Scout();
        vitals = new Vitals(character.getLevel(Skill.Resistance));
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
